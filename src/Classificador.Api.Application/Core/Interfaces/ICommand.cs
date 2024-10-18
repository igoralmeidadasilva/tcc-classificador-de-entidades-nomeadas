namespace Classificador.Api.Application.Core.Interfaces;

public interface ICommand<TResponse> : IRequest<TResponse> where TResponse : Result
{}
