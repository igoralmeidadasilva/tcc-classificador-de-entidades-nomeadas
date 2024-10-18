namespace Classificador.Api.Domain.Interfaces.Repositories.Persistence;

public interface IClassificationPersistenceRepository : IPersistenceRepository<Classification>
{
    Task UpdateStatusToCompletedAsync(Guid id, CancellationToken cancellationToken);
}
