using Classificador.Api.Domain.Core.Abstractions;

namespace Classificador.Api.Domain.Interfaces.Repositories.ReadOnly;

public interface IReadOnlyRepository<TEntity> where TEntity : Entity<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
