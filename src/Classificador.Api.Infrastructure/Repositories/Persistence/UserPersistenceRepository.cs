
namespace Classificador.Api.Infrastructure.Repositories.Persistence;

public class UserPersistenceRepository : BasePersistenceRepository<User>, IUserPersistenceRepository
{
    public UserPersistenceRepository(ClassifierContext context) : base(context)
    {
    }

}
