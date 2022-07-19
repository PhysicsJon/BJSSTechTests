package seleniumtests.utils;

import org.junit.jupiter.api.AfterAll;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.TestInstance;
import org.openqa.selenium.WebDriver;

@TestInstance(TestInstance.Lifecycle.PER_CLASS)
public abstract class WebDriverTestBase {
    private String uri = "https://localhost:5001";

    public WebDriver driver = new BrowserFactory("chrome").driver;

    @BeforeEach
    void beforeEach(){
        driver.get(uri);
    }

    @AfterAll
    void afterAll(){
        if(driver != null){
            driver.quit();
        }
    }
}
