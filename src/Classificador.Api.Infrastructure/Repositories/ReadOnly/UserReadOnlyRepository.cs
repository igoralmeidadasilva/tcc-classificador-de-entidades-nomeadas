namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public class UserReadOnlyRepository : BaseReadOnlyRepository<User>, IUserReadOnlyRepository
{
    public UserReadOnlyRepository(ClassifierContext context) : base(context)
    {
    }

}
