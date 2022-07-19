using OpenQA.Selenium;
using System;

namespace BJSSTechTestDotNet.CandidateTests.Utils
{
	public abstract class TestBase
	{
		private readonly LocalWebApplicationFactory factory;

		public Uri Url { get; }
		public IWebDriver Driver { get; protected set; }

		public TestBase(LocalWebApplicationFactory factory)
		{
			this.factory = factory;
			Url = new Uri(this.factory.RootUri);

			Driver = factory.Driver;

			Driver.Navigate().GoToUrl(Url);
		}
	}
}
