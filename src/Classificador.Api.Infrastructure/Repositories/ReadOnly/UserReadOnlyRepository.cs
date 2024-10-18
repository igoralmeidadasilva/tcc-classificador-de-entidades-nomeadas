using Classificador.Api.Domain.Core.Interfaces.Repositories.ReadOnly;

namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public class UserReadOnlyRepository : BaseReadOnlyRepository<User>, IUserReadOnlyRepository
{
    public UserReadOnlyRepository(IDbContextFactory<ClassifierContext> context) : base(context)
    {
    }

    public async Task<bool> IsEmailAlreadyExists(string email, CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Users.AsNoTracking().AnyAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        return (await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email, cancellationToken))!;
    }

}
