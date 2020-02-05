
using Application.Resources;
using FluentValidation;

public static class RuleBuilderExtensions
{
    public static IRuleBuilder<T, string> StrongPassword<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 14)
    {
        var options = ruleBuilder
                .NotEmpty().WithMessage(ValidationErrorMessages.PasswordEmpty)
                .MinimumLength(minimumLength).WithMessage(ValidationErrorMessages.PasswordLength)
                .Matches("[A-Z]").WithMessage(ValidationErrorMessages.PasswordUppercaseLetter)
                .Matches("[a-z]").WithMessage(ValidationErrorMessages.PasswordLowercaseLetter)
                .Matches("[0-9]").WithMessage(ValidationErrorMessages.PasswordDigit)
                .Matches("[^a-zA-Z0-9]").WithMessage(ValidationErrorMessages.PasswordSpecialCharacter);
        return options;
    }
}