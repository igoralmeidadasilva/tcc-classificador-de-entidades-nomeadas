namespace Classificador.Api.Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException() : base()
    { }
    protected DomainException(string? message) : base(message)
    { }
}
