using Classificador.Api.Domain.Core.Interfaces;

namespace Classificador.Api.Domain.Core.Abstractions;

public abstract class Entity<T> : IEntity<T>
{
    public Guid Id { get; init; }
    public DateTime CreatedOnUtc { get; init; }

    protected Entity() { } // ORM

    protected Entity(Guid id, DateTime createdOnUtc)
    {
        Id = id;
        CreatedOnUtc = createdOnUtc;
    }

    public abstract T Update(T entity);

}
