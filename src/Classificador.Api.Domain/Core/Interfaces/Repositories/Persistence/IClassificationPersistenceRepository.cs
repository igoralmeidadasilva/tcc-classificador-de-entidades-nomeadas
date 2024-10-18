namespace Classificador.Api.Domain.Core.Interfaces.Repositories.Persistence;

public interface IClassificationPersistenceRepository : IPersistenceRepository<Classification>
{
    Task UpdateStatusToCompletedAsync(Guid id, CancellationToken cancellationToken);
}
