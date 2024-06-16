namespace Classificador.Api.Application.Exceptions;

public abstract class InfrastructureException : Exception
{
    protected InfrastructureException(string? message) : base(message)
    { }
}
