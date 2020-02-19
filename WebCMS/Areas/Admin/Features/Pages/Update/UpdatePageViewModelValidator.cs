﻿using Application.Resources;
using FluentValidation;
using System.Collections.Generic;

namespace WebCMS.Areas.Admin.Features.Pages.Update
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