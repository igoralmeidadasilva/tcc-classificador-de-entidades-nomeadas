using Classificador.Api.Domain.Core.Interfaces.Repositories.ReadOnly;

namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public sealed class PrescribingInformationReadOnlyRepository
    : BaseReadOnlyRepository<PrescribingInformation>, IPrescribingInformationReadOnlyRepository
{
    public PrescribingInformationReadOnlyRepository(IDbContextFactory<ClassifierContext> context) : base(context)
    {
    }
    
    public async Task<IEnumerable<PrescribingInformation>> GetByNameOrDescriptionAsync(string name, CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.PrescribingsInformation
            .AsNoTracking()
            .Include(x => x.NamedEntities)
            .Where(x => x.Name.Contains(name) || x.Description!.Contains(name))
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public new async Task<IEnumerable<PrescribingInformation>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.PrescribingsInformation
            .AsNoTracking()
            .Include(x => x.NamedEntities)
            .ToListAsync(cancellationToken);
    }
}
