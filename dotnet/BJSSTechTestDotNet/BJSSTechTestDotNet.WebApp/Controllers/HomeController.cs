using BJSSTechTestDotNet.WebApp.Models;
using BJSSTechTestDotNet.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BJSSTechTestDotNet.WebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILoginService loginService;

		public HomeController(ILoginService loginService)
		{
			this.loginService = loginService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View(new LoginModel());
		}

		[HttpPost]
		public IActionResult Index(LoginModel model)
		{
			if (!ModelState.IsValid)
				return View(new LoginModel
				{
					UserName = model.UserName,
				});

			var result = loginService.CheckCredentials(
				new User
				{
					UserName = model.UserName,
					Password = model.Password,
				});

			if (result)
			{
				return RedirectToAction(nameof(Confirmation));
			}

			ModelState.AddModelError(nameof(model), "Invalid username/password");
			return View(new LoginModel
			{
				UserName = model.UserName,
			});
		}

		[HttpGet("confirmation")]
		public IActionResult Confirmation()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
