﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace BJSSTechTestDotNet.CandidateTests.Utils
{
    public sealed class BrowserFactory
	{
        private const string DefaultHubUrl = "http://localhost:4444/wd/hub";

        public BrowserFactory(string browser)
        {
            GridRunning = GetGridStatus();
            Driver = GetBrowser(browser);
        }

        public IWebDriver Driver { get; }

        public bool GridRunning { get; private set; }

        private static IWebDriver GetLocalChromeDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());

            return new ChromeDriver(GetChromeOptions(!Debugger.IsAttached));
        }

        private static IWebDriver GetChromeDriver(string hubURL)
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            return new RemoteWebDriver(new Uri(hubURL), GetChromeOptions(true));
        }

        private static IWebDriver GetFirefoxDriver(string hubURL)
        {
            new DriverManager().SetUpDriver(new FirefoxConfig());
            var options = new FirefoxOptions();
            options.AddArguments("headless", "window-size=1920,1080", "no-sandbox", "acceptInsecureCerts");

            return new RemoteWebDriver(new Uri(hubURL), options);
        }

        private static IWebDriver GetEdgeDriver()
        {
            new DriverManager().SetUpDriver(new EdgeConfig());
            var options = new EdgeOptions();
            options.AddArguments("start-maximized", "no-sandbox", "auto-open-devtools-for-tabs", "ignore-certificate-errors", "log-level=3");
            return new EdgeDriver(options);
        }

        private static bool GetGridStatus()
        {
            var requestUri = new Uri("http://localhost:4444/grid/console");
            using var httpClient = new HttpClient();
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUri);
            try
            {
                var response = httpClient.SendAsync(httpRequest).GetAwaiter().GetResult();

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        private static ChromeOptions GetChromeOptions(bool headless)
        {
            var options = new ChromeOptions();

            if (headless)
            {
                options.AddArguments("headless", "window-size=1920,1080", "no-sandbox", "ignore-certificate-errors", "log-level=3");
            }
            else
            {
                options.AddArguments("start-maximized", "no-sandbox", "auto-open-devtools-for-tabs", "ignore-certificate-errors", "log-level=3");
            }

            return options;
        }

        private IWebDriver GetBrowser(string browser)
        {
            if (Debugger.IsAttached || !GridRunning)
            {
                return GetLocalChromeDriver();
            }
            else
            {
                return browser.ToLower() switch
                {
                    "chrome" or "googlechrome" => GetChromeDriver(DefaultHubUrl),
                    "firefox" or "ff" or "mozilla" => GetFirefoxDriver(DefaultHubUrl),
                    "edge" or "msedge" or "ms" => GetEdgeDriver(),
                    _ => GetLocalChromeDriver(),
                };
            }
        }
    }
}
