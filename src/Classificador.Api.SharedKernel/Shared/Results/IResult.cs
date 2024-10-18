namespace Classificador.Api.SharedKernel.Shared.Results;

public interface IResult
{
    bool IsSuccess { get; }
    IList<Error> Errors { get; }
}
