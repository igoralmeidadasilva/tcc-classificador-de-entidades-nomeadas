namespace Classificador.Api.SharedKernel.Shared.Result;

public record Result<TValue> : Result, IResult
{
    public TValue? Value { get; init; } 

    protected internal Result(bool isSuccess, ICollection<Error> errors, TValue value = default!) : base(isSuccess, errors)
    {
        Value = value;
    }

    public static implicit operator Result<TValue>(TValue value)
    {
        return Success(value);
    }


    // public static Result<TValue> Success(TValue value)
    // {
    //     List<Error> errors = [];
    //     return new Result<TValue>(true, errors, value);
    // }

    // public static Result<TValue> Failure(Error error)
    // {
    //     return new Result<TValue>(false, [error]);
    // }

    // public static Result<TValue> Failure(ICollection<Error> errors)
    // {
    //     return new Result<TValue>(false, errors);
    // }

}
