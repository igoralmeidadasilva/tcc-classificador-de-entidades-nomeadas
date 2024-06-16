using Classificador.Api.Domain.Interfaces;

namespace Classificador.Api.Domain.Entities;

public abstract record Entity<T> : IEntity<T>
{
    public Guid Id { get; private set; }
    public DateTime CreatedAtOnUtc { get; private set; }
    public bool IsDeleted { get; private set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAtOnUtc = DateTime.UtcNow;
        IsDeleted = false;
    }

    public abstract T Update(T entity);

    public abstract void Validate();

    public virtual void Delete()
    {
        this.IsDeleted = true;
    }

    public virtual void Restore()
    {
        this.IsDeleted = false;
    }
}
