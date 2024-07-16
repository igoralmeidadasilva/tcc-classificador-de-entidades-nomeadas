namespace Classificador.Api.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : Result 
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators != null)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Count != 0)
            {
                List<PropertyFailure> errors = failures.Select(failure => new PropertyFailure(failure.ErrorCode ,failure.ErrorMessage)).ToList();
                var error = new ValidationError
                (
                    RequestValidationErrors.ValidationErrorCore(typeof(TRequest).Name), 
                    RequestValidationErrors.ValidationErrorMessage(), 
                    errors
                );

                return (TResponse)Result.Failure(error);
            }
        }

        return await next();
    }
}
