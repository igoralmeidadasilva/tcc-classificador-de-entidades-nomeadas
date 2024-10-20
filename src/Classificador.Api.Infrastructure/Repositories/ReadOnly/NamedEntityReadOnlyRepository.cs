using Classificador.Api.Domain.Core.Interfaces.Repositories.ReadOnly;

namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public class NamedEntityReadOnlyRepository : BaseReadOnlyRepository<NamedEntity>, INamedEntityReadOnlyRepository
{
    public NamedEntityReadOnlyRepository(IDbContextFactory<MedTaggerContext> context) : base(context)
    { }

    public async Task<IEnumerable<NamedEntity>> GetByPrescribingInformationAndUserAsync(
        Guid idPrescribingInformation, 
        Guid idUser,
        CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.NamedEntities
            .AsNoTracking()
            .Where(en => en.IdPrescribingInformation == idPrescribingInformation 
                && !context.Classifications.Any(c => c.IdNamedEntity == en.Id && c.IdUser == idUser))
            .OrderBy(en => en.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<NamedEntity>> GetByPrescribingInformationAsync(Guid idPrescribingInformation, CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.NamedEntities
            .AsNoTracking()
            .Where(en => en.IdPrescribingInformation.Equals(idPrescribingInformation))
            .ToListAsync(cancellationToken);
    }
}
