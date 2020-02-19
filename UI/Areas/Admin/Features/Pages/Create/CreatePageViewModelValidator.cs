using Application.Resources;
using FluentValidation;

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
