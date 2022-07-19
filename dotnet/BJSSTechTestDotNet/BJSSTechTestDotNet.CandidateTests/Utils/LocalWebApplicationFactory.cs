using BJSSTechTestDotNet.WebApp;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc.Testing;
using OpenQA.Selenium;
using System;
using System.IO;
using System.Linq;

namespace BJSSTechTestDotNet.CandidateTests.Utils
{
	public sealed class LocalWebApplicationFactory : WebApplicationFactory<Startup>
	{
		private const string LocalhostBaseAddress = "https://127.0.0.1";
        private const string Browser = "chrome";

		private readonly IWebHost host;

        public LocalWebApplicationFactory()
        {
            ClientOptions.BaseAddress = new Uri(LocalhostBaseAddress);

            host = CreateWebHostBuilder().Build();
            host.Start();

            RootUri = host.ServerFeatures.Get<IServerAddressesFeature>().Addresses.LastOrDefault();

            Driver = new BrowserFactory(Browser).Driver;
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var builder = WebHost.CreateDefaultBuilder(Array.Empty<string>());
            builder.UseWebRoot(Path.GetFullPath("../../../../BJSSTechTestDotNet.WebApp/wwwroot"));
            builder.UseStartup<Startup>();

            builder.UseUrls($"{LocalhostBaseAddress}:0");

            return builder;
        }

        public string RootUri { get; }
        public IWebDriver Driver { get; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                host?.Dispose();
                Driver?.Dispose();
            }
        }
    }
}
