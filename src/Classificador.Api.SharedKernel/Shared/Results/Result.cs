namespace Classificador.Api.SharedKernel.Shared.Results;

public record Result
{
    public bool IsSuccess { get; init; }

    public Error Error { get; init; }

    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success()
    {
        return new Result(true, Error.None);
    }

    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new Result<TValue>(true, Error.None, value);
    }

    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }

}
