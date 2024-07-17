namespace Classificador.Api.Infrastructure.Repositories.Persistence;

public sealed class CategoryPersistenceRepository : BasePersistenceRepository<Category>, ICategoryPersistenceRepository
{
    public CategoryPersistenceRepository(ClassifierContext context) : base(context)
    {
    }

}
