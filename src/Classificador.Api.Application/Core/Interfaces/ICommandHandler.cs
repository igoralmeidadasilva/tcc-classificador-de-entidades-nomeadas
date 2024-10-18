namespace Classificador.Api.Application.Core.Interfaces;

public interface ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
    where TResponse : Result
{}
