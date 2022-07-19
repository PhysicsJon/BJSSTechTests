using BJSSTechTestDotNet.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJSSTechTestDotNet.WebApp.Services
{
	public interface ILoginService
	{
		bool CheckCredentials(User user);
	}
}
