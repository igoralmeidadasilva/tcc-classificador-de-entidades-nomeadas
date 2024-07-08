using Classificador.Api.SharedKernel.Shared.Result;

namespace Classificador.Api.Application.Interfaces;

public interface ICommand<TResponse> : IRequest<TResponse> where TResponse : IResult
{
}
