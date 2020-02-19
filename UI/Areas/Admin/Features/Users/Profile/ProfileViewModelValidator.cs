using Application.Resources;
using FluentValidation;
using System.Collections.Generic;

namespace UI.Areas.Admin.Features.Users.Profile
{
    public class ProfileViewModelValidator : AbstractValidator<ProfileViewModel>
    {
        public List<string> allowedMimeTypes = new List<string>() { "png", "jpg" };

        public ProfileViewModelValidator()
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage(ValidationErrorMessages.EmailEmpty)
                .EmailAddress().WithMessage(ValidationErrorMessages.EmailNotValid);

            RuleFor(w => w.ProfilePicture).ValidImage().WithMessage(ValidationErrorMessages.InvalidImage);
        }
    }
}
