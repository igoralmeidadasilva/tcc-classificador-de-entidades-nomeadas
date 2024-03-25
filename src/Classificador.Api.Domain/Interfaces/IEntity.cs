namespace Classificador.Api.Domain.Interfaces;

public interface IEntity<T>
{
    public T Update(T entity);
    public void Validate();
}
