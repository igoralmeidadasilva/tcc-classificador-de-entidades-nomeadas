using MediatR;

namespace Classificador.Api.Application.Interfaces;

public interface IQuery<TResponse> : IRequest<TResponse>
{
}
