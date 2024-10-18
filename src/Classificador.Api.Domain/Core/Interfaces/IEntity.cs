namespace Classificador.Api.Domain.Core.Interfaces;

public interface IEntity<T>
{
    public Guid Id { get; }
    public DateTime CreatedOnUtc { get; }
    public T Update(T entity);
}
