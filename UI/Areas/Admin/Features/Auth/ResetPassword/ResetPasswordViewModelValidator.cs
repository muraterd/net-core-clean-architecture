using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Resources.Areas.Admin;

namespace UI.Areas.Admin.Features.Auth.ResetPassword
{
    public class ResetPasswordViewModelValidator : AbstractValidator<ResetPasswordViewModel>
    {
        public ResetPasswordViewModelValidator()
        {
            RuleFor(w => w.Token)
                .NotNull().WithMessage(ValidationErrorMessages.GenericRequiredMessage);

            RuleFor(x => x.NewPassword).NotEmpty().WithMessage(ValidationErrorMessages.PasswordEmpty);

            RuleFor(x => x.NewPasswordConfirm)
                .Equal(x => x.NewPassword).WithMessage(ValidationErrorMessages.PasswordConfirmNotMatched);
        }
    }
}
