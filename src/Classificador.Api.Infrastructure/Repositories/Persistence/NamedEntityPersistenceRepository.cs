namespace Classificador.Api.Infrastructure.Repositories.Persistence;

public class NamedEntityPersistenceRepository : BasePersistenceRepository<NamedEntity>, INamedEntityPersistenceRepository
{
    public NamedEntityPersistenceRepository(ClassifierContext context) : base(context)
    {
    }

}
