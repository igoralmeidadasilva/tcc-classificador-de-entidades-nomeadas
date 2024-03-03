namespace Classificador.Api.Domain.Entities.Base;

public abstract class EntityBase<T> where T : EntityBase<T>
{
    public int Id { get; init; }
    public DateTime CreatedAt { get; init; }

    public EntityBase()
    {
        CreatedAt = DateTime.Now;
    }

    public EntityBase(int id) : base()
    {
        Id = id;
    }
}
