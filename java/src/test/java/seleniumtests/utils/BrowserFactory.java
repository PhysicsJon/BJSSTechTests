package seleniumtests.utils;

import io.github.bonigarcia.wdm.WebDriverManager;
import org.jetbrains.annotations.NotNull;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeOptions;
import org.openqa.selenium.firefox.FirefoxDriver;

import java.util.Locale;

import static io.github.bonigarcia.wdm.config.DriverManagerType.*;

public final class BrowserFactory {
    public WebDriver driver;
    
    public BrowserFactory(String browserName) {
        setDriver(browserName);
    }

    private void setDriver(@NotNull String browserName) {
        switch (browserName.toLowerCase(Locale.ROOT)) {
            case "firefox" :
            case "ff" :
            case "mozilla" :
                setFirefoxDriver();
                break;
            default :
                setChromeDriver();
        }
    }

    private void setFirefoxDriver() {
        WebDriverManager.getInstance(FIREFOX).setup();
        driver = new FirefoxDriver();
    }

    private void setChromeDriver() {
        WebDriverManager.getInstance(CHROME).setup();
        ChromeOptions options = new ChromeOptions();
        if (debuggerAttached()) {
            options.addArguments("start-maximized", "auto-open-devtools-for-tabs");
        } else {
            options.addArguments("headless", "window-size=1920,1080");
        }

        options.addArguments("no-sandbox", "ignore-certificate-errors");
        driver = new ChromeDriver(options);
    }

    private static boolean debuggerAttached() {
        return java.lang.management.ManagementFactory.getRuntimeMXBean().getInputArguments().toString().contains("jdwp");
    }
}