
namespace Classificador.Api.Infrastructure.Repositories.Persistence;

public sealed class ClassificationPersistenceRepository : BasePersistenceRepository<Classification>, IClassificationPersistenceRepository
{
    public ClassificationPersistenceRepository(ClassifierContext context) : base(context)
    {
    }

    public async Task UpdateStatusToCompletedAsync(Guid id, CancellationToken cancellationToken)
    {
        await _context.Classifications
            .Where(cla => cla.Id.Equals(id))
            .ExecuteUpdateAsync(x => x.SetProperty(p => p.Status, ClassificationStatus.Completo), cancellationToken);
    }
}
