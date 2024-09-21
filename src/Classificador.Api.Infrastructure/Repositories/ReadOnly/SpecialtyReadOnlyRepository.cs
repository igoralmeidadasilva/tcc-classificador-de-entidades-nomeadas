namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public sealed class SpecialtyReadOnlyRepository : BaseReadOnlyRepository<Specialty>, ISpecialtyReadOnlyRepository
{
    public SpecialtyReadOnlyRepository(IDbContextFactory<ClassifierContext> context) : base(context)
    {
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Specialties.AsNoTracking().AnyAsync(x => x.Name == name, cancellationToken);
    }
}