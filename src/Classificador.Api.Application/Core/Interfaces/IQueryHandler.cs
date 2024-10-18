namespace Classificador.Api.Application.Core.Interfaces;

public interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> 
    where TRequest : IQuery<TResponse>
    where TResponse : Result
{}
