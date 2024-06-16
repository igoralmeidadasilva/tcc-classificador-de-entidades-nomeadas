namespace Classificador.Api.SharedKernel.Shared;

public sealed record Result
{
    public object Data { get; init; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    private Result(bool isSuccess, Error error, object data = default!)
    {
        IsSuccess = isSuccess;
        Error = error;
        Data = data;
    }

    public static Result Success(object data)
    {
        return new Result(true, Error.None, data);
    }

    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }
}
