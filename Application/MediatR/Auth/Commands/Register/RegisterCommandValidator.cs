﻿using FluentValidation;

namespace Application.MediatR.Auth.Commands.Register
{
	public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
	{
		public RegisterCommandValidator()
		{
			RuleFor(x => x.Email).NotNull().EmailAddress();
			RuleFor(x => x.Password).NotNull();
		}
	}
}
