namespace Classificador.Api.Application.Extensions;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, PropertyFailure error)
    {
        if (error is null)
            throw new ArgumentNullException(nameof(error), "The error is required");
        
        return rule.WithErrorCode(error.Failure).WithMessage(error.Description);
    }

}
