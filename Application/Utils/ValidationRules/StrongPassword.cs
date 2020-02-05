using FluentValidation;

public static class RuleBuilderExtensions
{
    public static IRuleBuilder<T, string> StrongPassword<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 14)
    {
        var options = ruleBuilder
            .NotEmpty()
            .MinimumLength(minimumLength)
            .Matches("[A-Z]")
            .Matches("[a-z]")
            .Matches("[0-9]")
            .Matches("[^a-zA-Z0-9]");
        return options;
    }
}