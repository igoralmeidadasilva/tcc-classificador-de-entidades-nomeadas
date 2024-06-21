namespace Classificador.Api.Domain.Exceptions;

public sealed class EntityNotFoundException : DomainException
{
    public EntityNotFoundException()
    { }

    public EntityNotFoundException(string? message) : base(message)
    { }
}
