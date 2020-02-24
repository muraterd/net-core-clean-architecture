using Application.Resources;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.MediatR.Admin.Auth.Commands
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(w => w.Token).NotEmpty();

            RuleFor(w => w.NewPassword).NotEmpty();
        }
    }
}
