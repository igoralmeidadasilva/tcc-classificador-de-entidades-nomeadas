using Classificador.Api.Domain.Entities;

namespace Classificador.Api.Domain.Interfaces.Repositories;

public interface IPersistenceRepository<TEntity> where TEntity : Entity<TEntity>
{
    Task<Guid> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<Guid> AddRangeAsync (IEnumerable<TEntity> entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
