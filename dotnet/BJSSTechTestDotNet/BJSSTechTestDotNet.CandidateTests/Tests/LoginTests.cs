using BJSSTechTestDotNet.CandidateTests.Utils;
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
			Driver.FindElement(By.Id("example_id"));
		}
	}
}
