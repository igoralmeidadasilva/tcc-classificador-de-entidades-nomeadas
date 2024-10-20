using Classificador.Api.Domain.Core.Interfaces.Repositories.Persistence;

namespace Classificador.Api.Infrastructure.Repositories.Persistence;

public class UserPersistenceRepository : BasePersistenceRepository<User>, IUserPersistenceRepository
{
    public UserPersistenceRepository(IDbContextFactory<MedTaggerContext> contextFactory) : base(contextFactory)
    {
    }

}
