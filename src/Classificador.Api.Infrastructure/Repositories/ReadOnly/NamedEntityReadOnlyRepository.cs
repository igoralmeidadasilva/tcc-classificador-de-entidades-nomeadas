namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public class NamedEntityReadOnlyRepository : BaseReadOnlyRepository<NamedEntity>, INamedEntityReadOnlyRepository
{
    public NamedEntityReadOnlyRepository(ClassifierContext context) : base(context)
    {
    }

    public async Task<IEnumerable<NamedEntity>> GetByPrescribingInformationIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.NamedEntities
            .AsNoTracking()
            .Where(x => x.IdPrescribingInformation.Equals(id))
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

}
