namespace Classificador.Api.Application.Extensions;

public static class ValidationErrorExtensions
{
    public static string ExtractValidationErrors(this ValidationError errors, string codeStartPrefix)
    {

        IEnumerable<string> descriptions = errors.Failures
                .Where(failure => failure.Failure.StartsWith(codeStartPrefix))
                .Select(failure => failure.Description);

        return string.Join(Environment.NewLine, descriptions);
    }
}