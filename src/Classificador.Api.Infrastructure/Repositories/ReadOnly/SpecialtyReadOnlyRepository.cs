namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public sealed class SpecialtyReadOnlyRepository : BaseReadOnlyRepository<Specialty>, ISpecialtyReadOnlyRepository
{
    public SpecialtyReadOnlyRepository(ClassifierContext context) : base(context)
    {
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Specialties.AsNoTracking().AnyAsync(x => x.Name == name, cancellationToken);
    }
}