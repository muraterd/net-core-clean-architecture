using FluentValidation;
using System;
using UI.Resources.Areas.Admin;

public class StrongPasswordOptions
{
    public int MinimumLength = 8;
    public bool RequireUpperCase = true;
    public bool RequireLowerCase = true;
    public bool RequireDigit = true;
    public bool RequireSpecialCharacter = true;
}

public static class RuleBuilderExtensions
{
    public static IRuleBuilder<T, string> StrongPassword<T>(this IRuleBuilder<T, string> ruleBuilder, Action<StrongPasswordOptions> options)
    {
        var defaultOptions = new StrongPasswordOptions();
        options?.Invoke(defaultOptions);

        ruleBuilder
            .NotEmpty().WithMessage(ValidationErrorMessages.PasswordEmpty)
            .MinimumLength(defaultOptions.MinimumLength).WithMessage(String.Format(ValidationErrorMessages.PasswordLength, defaultOptions.MinimumLength));

        if (defaultOptions.RequireUpperCase)
        {
            ruleBuilder.Matches("[A-Z]").WithMessage(ValidationErrorMessages.PasswordUppercaseLetter);
        }

        if (defaultOptions.RequireLowerCase)
        {
            ruleBuilder.Matches("[a-z]").WithMessage(ValidationErrorMessages.PasswordLowercaseLetter);
        }

        if (defaultOptions.RequireDigit)
        {
            ruleBuilder.Matches("[0-9]").WithMessage(ValidationErrorMessages.PasswordDigit);
        }

        if (defaultOptions.RequireSpecialCharacter)
        {
            ruleBuilder.Matches("[^a-zA-Z0-9]").WithMessage(ValidationErrorMessages.PasswordSpecialCharacter);
        }

        return ruleBuilder;
    }
}