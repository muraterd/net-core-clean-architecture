using Application.Resources;
using FluentValidation;

namespace Application.MediatR.Admin.Auth.Commands.CreateSuperAdmin
{
    public class CreateSuperAdminCommandValidator : AbstractValidator<CreateSuperAdminCommand>
    {
        public CreateSuperAdminCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage(ValidationErrorMessages.EmailEmpty)
                .EmailAddress().WithMessage(ValidationErrorMessages.EmailNotValid);

            RuleFor(x => x.Password).StrongPassword(o =>
            {
                o.MinimumLength = 4; 
                o.RequireSpecialCharacter = false;
                o.RequireUpperCase = false;
                o.RequireLowerCase = false;
                o.RequireDigit = false;
            });

            RuleFor(x => x.PasswordConfirm)
                .Equal(x => x.Password).WithMessage(ValidationErrorMessages.PasswordConfirmNotMatched);
        }
    }
}
