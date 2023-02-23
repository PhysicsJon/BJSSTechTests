using System.Linq;
using BJSSTechTestDotNet.CandidateTests.Utils;
using FluentAssertions;
using FluentAssertions.Execution;
using OpenQA.Selenium;
using Xunit;

namespace BJSSTechTestDotNet.CandidateTests.Tests
{
	public class LoginTests : TestBase, IClassFixture<LocalWebApplicationFactory>
	{
		public LoginTests(LocalWebApplicationFactory factory)
			: base(factory)
		{
		}

		/*
		 *  Please feel free to run the application (press F5) at any time to check the functionality of the page under test
		 *  Correct login details are as follows
		 *	candidate@bjss.com	Test123
		 * 
		 * */

		[Fact]
		public void LoginTest_ExampleTest()
		{
			// Provided here is an example of how the driver is set up and ready to use.
			Driver.FindElement(By.Id("UserName")).SendKeys("candidate@bjss.com");
			Driver.FindElement(By.XPath("//input[@type='password']")).SendKeys("Test123");
			Driver.FindElement(By.XPath("//button[@type='submit']")).Click();
			var textSuccessMessage = Driver.FindElement(By.XPath("//div[@class='alert alert-success']")).Text;
			textSuccessMessage.Should().Be("Welcome to the confirmation screen");

		}

		// Invalid username
			[Fact]
			public void InvalidPassword()
			{
				Driver.FindElement(By.Id("UserName")).SendKeys("candidate@bjss.com");
				Driver.FindElement(By.XPath("//input[@type='password']")).SendKeys("Test12");
				Driver.FindElement(By.XPath("//button[@type='submit']")).Click();
				Driver.FindElement(By.CssSelector("div.validation-summary-errors")).Displayed.Should().BeTrue();
				Driver.FindElement(By.CssSelector("div.validation-summary-errors li")).Text.Should().Be("Invalid username/password");

			}

			[Fact]		// invalid password

			public void InvalidUserNmae()
			{
				Driver.FindElement(By.Id("UserName")).SendKeys("Abc@bjss.com");
				Driver.FindElement(By.XPath("//input[@type='password']")).SendKeys("Test123");
				Driver.FindElement(By.XPath("//button[@type='submit']")).Click();
				Driver.FindElement(By.CssSelector("div.validation-summary-errors")).Displayed.Should().BeTrue();
				Driver.FindElement(By.CssSelector("div.validation-summary-errors li")).Text.Should().Be("Invalid username/password");

			}
		// empty username
		[Fact]
		public void EmptyUserName()
		{
				Driver.FindElement(By.Id("UserName")).SendKeys("");
				Driver.FindElement(By.XPath("//input[@type='password']")).SendKeys("Test123");
				Driver.FindElement(By.XPath("//button[@type='submit']")).Click();
				Driver.FindElement(By.CssSelector("div.validation-summary-errors")).Displayed.Should().BeTrue();
				Driver.FindElement(By.CssSelector("div.validation-summary-errors li")).Text.Should().Be("Email cannot be empty");
				Driver.FindElement(By.CssSelector("span.field-validation-error")).Text.Should().Be("Email cannot be empty");			

		}
		// empty password
		[Fact]
		public void EmptyPassword()
		{
				Driver.FindElement(By.Id("UserName")).SendKeys("Abc@bjss.com");
				Driver.FindElement(By.XPath("//input[@type='password']")).SendKeys("");
				Driver.FindElement(By.XPath("//button[@type='submit']")).Click();
				using(new AssertionScope()){
					Driver.FindElement(By.CssSelector("div.validation-summary-errors")).Displayed.Should().BeTrue();
					Driver.FindElement(By.CssSelector("div.validation-summary-errors li")).Text.Should().Be("Password cannot be empty");
					Driver.FindElement(By.CssSelector("span.field-validation-error")).Text.Should().Be("Password cannot be empty");			
				}
		}

		[Fact]
		public void EmptyUserNamePassword()
		{
			Driver.FindElement(By.Id("UserName")).SendKeys("");
				Driver.FindElement(By.XPath("//input[@type='password']")).SendKeys("");
				Driver.FindElement(By.XPath("//button[@type='submit']")).Click();
				var errorSummaryErrors = Driver.FindElements(By.CssSelector("div.validation-summary-errors li")).Select(element => element.Text).ToList();
		}
	}
}
