namespace Classificador.Api.Application.Interfaces;

public interface IQuery<TResponse> : IRequest<TResponse> where TResponse : Result
{
}
