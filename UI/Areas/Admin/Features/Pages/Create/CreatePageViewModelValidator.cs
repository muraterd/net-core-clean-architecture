using FluentValidation;
using UI.Resources.Areas.Admin;

namespace UI.Areas.Admin.Features.Pages.Create
{
    public class CreatePageViewModelValidator : AbstractValidator<CreatePageViewModel>
    {
        public CreatePageViewModelValidator()
        {
            RuleFor(x => x.Slug)
                .NotNull().WithMessage(ValidationErrorMessages.GenericRequiredMessage);
        }
    }
}
