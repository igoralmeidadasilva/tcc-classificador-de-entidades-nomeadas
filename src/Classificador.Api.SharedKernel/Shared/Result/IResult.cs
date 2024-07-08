namespace Classificador.Api.SharedKernel.Shared.Result;

public interface IResult
{
    public bool IsSuccess { get; }
    public ICollection<Error> Errors { get; }
}
