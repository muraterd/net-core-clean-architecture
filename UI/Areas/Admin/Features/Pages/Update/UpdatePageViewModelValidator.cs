using FluentValidation;
using UI.Resources.Areas.Admin;

namespace UI.Areas.Admin.Features.Pages.Update
{
    public class UpdatePageViewModelValidator : AbstractValidator<UpdatePageViewModel>
    {
        public UpdatePageViewModelValidator()
        {
            RuleFor(x => x.Slug)
                .NotNull().WithMessage(ValidationErrorMessages.GenericRequiredMessage);
        }
    }
}
