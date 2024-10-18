namespace Classificador.Api.Application.Core.Interfaces;

public interface IQuery<TResponse> : IRequest<TResponse> where TResponse : Result
{}
