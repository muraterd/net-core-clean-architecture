using Application.Resources;
using FluentValidation;

namespace Application.MediatR.Api.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmailEmpty)
                .EmailAddress().WithMessage(ValidationErrorMessages.EmailNotValid);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationErrorMessages.PasswordEmpty);
        }
    }
}
