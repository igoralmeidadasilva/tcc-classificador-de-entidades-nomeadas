namespace Classificador.Api.Infrastructure.Repositories.Persistence;

public class UserPersistenceRepository : BasePersistenceRepository<User>, IUserPersistenceRepository
{
    public UserPersistenceRepository(IDbContextFactory<ClassifierContext> contextFactory) : base(contextFactory)
    {
    }

}
