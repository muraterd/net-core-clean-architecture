using FluentValidation;
using FluentValidation.Validators;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public static class FileValidators
{
    public static IRuleBuilderOptions<T, IFormFile> ValidImage<T>(this IRuleBuilder<T, IFormFile> ruleBuilder)
    {
        return ruleBuilder.MustAsync(CheckIfImageIsValid);
    }

    private static async Task<bool> CheckIfImageIsValid(IFormFile file, CancellationToken cancellationToken)
    {
        if(file == null)
        {
            return true;
        }

        return await new ImageValidator().Validate(file);
    }
}