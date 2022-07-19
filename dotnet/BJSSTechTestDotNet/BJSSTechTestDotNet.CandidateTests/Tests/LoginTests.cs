using BJSSTechTestDotNet.CandidateTests.Utils;
using FluentAssertions;
using FluentAssertions.Execution;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BJSSTechTestDotNet.CandidateTests.Tests
{
    public class LoginTests : TestBase, IClassFixture<LocalWebApplicationFactory>
    {
        private IWebElement usernameField;
        private IWebElement passwordField;
        private IWebElement loginButton;
        private string correctUsername = "candidate@bjss.com";
        private string correctPassword = "Test123";

        public LoginTests(LocalWebApplicationFactory factory)
            : base(factory)
        {
            usernameField = Driver.FindElement(By.Id("UserName"));
            passwordField = Driver.FindElement(By.Id("Password"));
            loginButton = Driver.FindElement(By.ClassName("btn-primary"));
        }

        /*
         *  Please feel free to run the application (press F5) at any time to check the functionality of the page under test
         *  Correct login details are as follows
         *	candidate@bjss.com	Test123
         * 
         * */

        [Fact(Skip = "ignore this")]
        public void LoginTest_ExampleTest()
        {
            // Provided here is an example of how the driver is set up and ready to use.
            Driver.FindElement(By.Id("example_id"));
        }

        [Fact]
        public void FieldsAndButtonsAreEnabledWhenPageLoads()
        {
            using (new AssertionScope())
            {
                usernameField.Displayed.Should().BeTrue();
                usernameField.Enabled.Should().BeTrue();
                passwordField.Displayed.Should().BeTrue();
                passwordField.Enabled.Should().BeTrue();
                loginButton.Displayed.Should().BeTrue();
                loginButton.Displayed.Should().BeTrue();
            }
        }

        [Fact]
        public void ErrorsAreDisplayedWhenAttemptingToLoginWithInvalidUsernameAndNoPassword()
        {
            usernameField.SendKeys("abc");
            loginButton.Click();

            IEnumerable<IWebElement> errorList = Driver.FindElements(By.CssSelector("div.validation-summary-errors li"));
            IWebElement emailFieldError = Driver.FindElement(By.CssSelector("span[data-valmsg-for='UserName']"));
            IWebElement passwordFieldError = Driver.FindElement(By.CssSelector("span[data-valmsg-for='Password']"));

            using (new AssertionScope())
            {
                errorList.Select(e => e.Text).Should().BeEquivalentTo(new[] { "Email is not in a valid format", "Password cannot be empty" });
                emailFieldError.Displayed.Should().BeTrue();
                emailFieldError.Text.Should().BeEquivalentTo("Email is not in a valid format");
                passwordFieldError.Displayed.Should().BeTrue();
                passwordFieldError.Text.Should().BeEquivalentTo("Password cannot be empty");
            }
        }

        [Fact]
        public void ErrorsAreDiplayedWhenUserAttemptsToLoginWithoutEnteringPassword()
        {
            usernameField.SendKeys(correctUsername);
            loginButton.Click();

            IEnumerable<IWebElement> errorList = Driver.FindElements(By.CssSelector("div.validation-summary-errors li"));
            IWebElement emailFieldError = Driver.FindElement(By.CssSelector("span[data-valmsg-for='UserName']"));
            IWebElement passwordFieldError = Driver.FindElement(By.CssSelector("span[data-valmsg-for='Password']"));

            using (new AssertionScope())
            {
                errorList.Select(e => e.Text).Should().BeEquivalentTo(new[] { "Password cannot be empty" });
                emailFieldError.Displayed.Should().BeFalse();
                passwordFieldError.Displayed.Should().BeTrue();
                passwordFieldError.Text.Should().BeEquivalentTo("Password cannot be empty");
            }
        }

        [Fact]
        public void ErrorsAreDisplayedWhenNoUserNameAndPasswordIsEntered()
        {
            loginButton.Click();

            IEnumerable<IWebElement> errorList = Driver.FindElements(By.CssSelector("div.validation-summary-errors li"));
            IWebElement emailFieldError = Driver.FindElement(By.CssSelector("span[data-valmsg-for='UserName']"));
            IWebElement passwordFieldError = Driver.FindElement(By.CssSelector("span[data-valmsg-for='Password']"));

            using (new AssertionScope())
            {
                errorList.Select(e => e.Text).Should().BeEquivalentTo(new[] { "Email cannot be empty", "Password cannot be empty" });
                emailFieldError.Displayed.Should().BeTrue();
                emailFieldError.Text.Should().BeEquivalentTo("Email cannot be empty");
                passwordFieldError.Displayed.Should().BeTrue();
                passwordFieldError.Text.Should().BeEquivalentTo("Password cannot be empty");
            }
        }

        //Valid user name invalid password

        [Fact]
        public void ErrorIsDisplayedWhenAValidUsernameAndInvalidPasswordAreEntered()
        {
            usernameField.SendKeys(correctUsername);
            passwordField.SendKeys(correctPassword + "1");
            loginButton.Click();

            IEnumerable<IWebElement> errorList = Driver.FindElements(By.CssSelector("div.validation-summary-errors li"));
            IWebElement emailFieldError = Driver.FindElement(By.CssSelector("span[data-valmsg-for='UserName']"));
            IWebElement passwordFieldError = Driver.FindElement(By.CssSelector("span[data-valmsg-for='Password']"));

            errorList.Select(e => e.Text).Should().BeEquivalentTo(new[] { "Invalid username/password" });
        }

        [Fact]
        public void ErrorsAreDisplayedWhenNoUsernameAndAValidPasswordAreEntered()
        {
            passwordField.SendKeys(correctPassword);
            loginButton.Click();

            IEnumerable<IWebElement> errorList = Driver.FindElements(By.CssSelector("div.validation-summary-errors li"));
            IWebElement emailFieldError = Driver.FindElement(By.CssSelector("span[data-valmsg-for='UserName']"));
            IWebElement passwordFieldError = Driver.FindElement(By.CssSelector("span[data-valmsg-for='Password']"));

            using (new AssertionScope())
            {
                errorList.Select(e => e.Text).Should().BeEquivalentTo(new[] { "Email cannot be empty" });
                emailFieldError.Displayed.Should().BeTrue();
                emailFieldError.Text.Should().BeEquivalentTo("Email cannot be empty");
            }
        }

        [Fact]
        public void SuccessfulLogin()
        {
            usernameField.SendKeys(correctUsername);
            passwordField.SendKeys(correctPassword);
            loginButton.Click();
            IWebElement confirmationMessage = Driver.FindElement(By.ClassName("alert-success"));

            using (new AssertionScope())
            {
                confirmationMessage.Displayed.Should().BeTrue();
                confirmationMessage.Text.Should().BeEquivalentTo("Welcome to the confirmation screen");
            }
        }
    }
}
