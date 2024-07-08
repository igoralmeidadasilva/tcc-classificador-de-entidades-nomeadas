namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public class UserReadOnlyRepository : BaseReadOnlyRepository<User>, IUserReadOnlyRepository
{
    public UserReadOnlyRepository(ClassifierContext context) : base(context)
    {
    }

    public async Task<bool> IsEmailAlreadyExists(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users.AsNoTracking().AnyAsync(x => x.Email == email, cancellationToken);
    }

}
