using FluentValidation;

namespace Application.MediatR.Auth.Commands.Login
{
	public class LoginCommandValidator : AbstractValidator<LoginCommand>
	{
		public LoginCommandValidator()
		{
			RuleFor(x => x.Email).NotNull().EmailAddress();
			RuleFor(x => x.Password).NotNull();
		}
	}
}
