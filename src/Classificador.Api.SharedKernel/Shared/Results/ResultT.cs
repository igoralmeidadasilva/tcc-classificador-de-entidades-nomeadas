namespace Classificador.Api.SharedKernel.Shared.Results;

public record Result<TValue> : Result
{
    public TValue? Value { get; init; } 

    protected internal Result(bool isSuccess, Error error, TValue value = default!) : base(isSuccess, error)
    {
        Value = value;
    }

    public static implicit operator Result<TValue>(TValue value)
    {
        return Success(value);
    }

}
