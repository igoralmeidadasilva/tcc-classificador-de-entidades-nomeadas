
using Classificador.Api.Domain.Core.Enums;

namespace Classificador.Api.Infrastructure.Repositories.Persistence;

public sealed class ClassificationPersistenceRepository : BasePersistenceRepository<Classification>, IClassificationPersistenceRepository
{
    public ClassificationPersistenceRepository(IDbContextFactory<ClassifierContext> contextFactory) : base(contextFactory)
    {
    }

    public async Task UpdateStatusToCompletedAsync(Guid id, CancellationToken cancellationToken)
    {
        using var context = _contextFactory.CreateDbContext();
        await context.Classifications
            .Where(cla => cla.Id.Equals(id))
            .ExecuteUpdateAsync(x => x.SetProperty(p => p.Status, ClassificationStatus.Completo), cancellationToken);
    }
}
