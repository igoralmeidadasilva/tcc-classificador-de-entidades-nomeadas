namespace Classificador.Api.Application.Extensions;

public static class ValidationErrorExtensions
{
    public static List<string> ExtractValidationErrors(this ValidationError errors, string codeStartPrefix)
    {
        List<string> descriptions = errors.Failures
                .Where(failure => failure.Failure.StartsWith(codeStartPrefix))
                .Select(failure => failure.Description)
                .ToList();

        return descriptions;
    }
}