using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Resources.Areas.Admin;

namespace UI.Areas.Admin.Features.Auth.Register
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(ValidationErrorMessages.GenericRequiredMessage);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(ValidationErrorMessages.GenericRequiredMessage);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmailEmpty)
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
