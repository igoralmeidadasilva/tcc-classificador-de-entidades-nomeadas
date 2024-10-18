namespace Classificador.Api.Application.Core.Extensions;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, Error error) 
        => error is null ? throw new ArgumentNullException(nameof(error), "The error is required") : rule.WithErrorCode(error.Code).WithMessage(error.Message);
}
