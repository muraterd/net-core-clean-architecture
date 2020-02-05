using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.MediatR.Auth.Commands.CreateSuperAdmin
{
    public class CreateSuperAdminCommandValidator : AbstractValidator<CreateSuperAdminCommand>
    {
        public CreateSuperAdminCommandValidator()
        {
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.Password).StrongPassword(10);
        }
    }

    
}
