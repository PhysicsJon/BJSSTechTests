package seleniumtests.tests;

import org.junit.jupiter.api.Test;
import org.openqa.selenium.By;
import seleniumtests.utils.WebDriverTestBase;

public class LoginTests extends WebDriverTestBase {
    /*
     *  The Driver is already set up, there is no need to create your own.
     *
     *  Correct login details are as follows
     *	candidate@bjss.com	Test123
     *
     * */

    @Test
    public void loginTests_ExampleTest()
    {
        // Provided here is an example of how the driver is set up and ready to use.
        driver.findElement(By.id("example_id"));
    }
}
