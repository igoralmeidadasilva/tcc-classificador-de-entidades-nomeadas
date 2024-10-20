using Classificador.Api.Domain.Core.Interfaces.Repositories.Persistence;

namespace Classificador.Api.Infrastructure.Repositories.Persistence;

public sealed class CategoryPersistenceRepository : BasePersistenceRepository<Category>, ICategoryPersistenceRepository
{
    public CategoryPersistenceRepository(IDbContextFactory<MedTaggerContext> contextFactory) : base(contextFactory)
    {
    }

}
