using BJSSTechTestDotNet.WebApp.Models;
using FluentValidation;

namespace BJSSTechTestDotNet.WebApp.Validators
{
	public sealed class LoginModelValidator : AbstractValidator<LoginModel>
	{
		public LoginModelValidator()
		{
			RuleFor(m => m.UserName)
				.NotEmpty()
				.WithMessage("Email cannot be empty")
				.EmailAddress()
				.WithMessage("Email is not in a valid format");

			RuleFor(m => m.Password)
				.NotEmpty()
				.WithMessage("Password cannot be empty");
		}
	}
}
