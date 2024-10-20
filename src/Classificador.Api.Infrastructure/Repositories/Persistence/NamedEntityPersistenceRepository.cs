using Classificador.Api.Domain.Core.Interfaces.Repositories.Persistence;

namespace Classificador.Api.Infrastructure.Repositories.Persistence;

public class NamedEntityPersistenceRepository : BasePersistenceRepository<NamedEntity>, INamedEntityPersistenceRepository
{
    public NamedEntityPersistenceRepository(IDbContextFactory<MedTaggerContext> contextFactory) : base(contextFactory)
    {
    }

}
