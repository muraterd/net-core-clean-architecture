using Application.Resources;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Areas.Admin.Features.Auth.ForgotPassword
{
    public class ForgotPasswordViewModelValidator : AbstractValidator<ForgotPasswordViewModel>
    {
        public ForgotPasswordViewModelValidator()
        {
            RuleFor(w => w.Email)
                .NotNull().WithMessage(ValidationErrorMessages.EmailEmpty)
                .EmailAddress().WithMessage(ValidationErrorMessages.EmailNotValid);
        }
    }
}
