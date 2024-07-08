namespace Classificador.Api.SharedKernel.Shared.Result;

public record Result : IResult
{
    public bool IsSuccess { get; }

    public ICollection<Error> Errors { get; } = [];

    protected Result(bool isSuccess, ICollection<Error> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static Result Success()
    {
        List<Error> errors = [];
        return new Result(true, errors);
    }

    public static Result<TValue> Success<TValue>(TValue value)
    {
        List<Error> errors = [];
        return new Result<TValue>(true, errors, value);
    }

    public static Result Failure(Error error)
    {
        return new Result(false, [error]);
    }

    public static Result Failure(ICollection<Error> errors)
    {
        return new Result(false, errors);
    }
}
