using FluentValidation;

namespace Application.MediatR.Admin.User.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.Email)
                .NotNull()
                .EmailAddress();
        }
    }
}
