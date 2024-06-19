namespace Classificador.Api.Domain.Entities;

public abstract class Entity<T> : IEntity<T>
{
    public Guid Id { get; init; }
    public DateTime CreatedAtOnUtc { get; init; }
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
