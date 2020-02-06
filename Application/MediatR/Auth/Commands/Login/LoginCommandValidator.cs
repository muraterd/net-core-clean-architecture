using Application.Resources;
using FluentValidation;

namespace Application.MediatR.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<AdminLoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmailEmpty)
                .EmailAddress().WithMessage(ValidationErrorMessages.EmailEmpty);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationErrorMessages.PasswordEmpty);
        }
    }
}
