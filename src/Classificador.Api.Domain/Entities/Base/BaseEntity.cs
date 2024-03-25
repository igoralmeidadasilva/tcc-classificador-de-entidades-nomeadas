using Classificador.Api.Domain.Interfaces;

namespace Classificador.Api.Domain.Entities.Base;

public abstract record BaseEntity<T> where T : BaseEntity<T>, IEntity<T>
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? LastUpdate { get; protected set; }

    protected BaseEntity(Guid id)
    {
        Id = id;
        CreatedAt = DateTime.Now;
    }

    protected BaseEntity()
    { }
    public abstract T Update(T entity);
    public abstract void Validate();

}
