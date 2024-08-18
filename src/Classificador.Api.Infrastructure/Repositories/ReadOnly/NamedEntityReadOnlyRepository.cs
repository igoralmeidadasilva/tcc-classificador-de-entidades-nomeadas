namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public class NamedEntityReadOnlyRepository : BaseReadOnlyRepository<NamedEntity>, INamedEntityReadOnlyRepository
{
    public NamedEntityReadOnlyRepository(ClassifierContext context) : base(context)
    { }

    public async Task<IEnumerable<NamedEntity>> GetByPrescribingInformationIdAsync(
        Guid idPrescribingInformation, 
        Guid idUser,
        CancellationToken cancellationToken)
    {
        return await _context.NamedEntities
            .AsNoTracking()
            .Where(en => en.IdPrescribingInformation == idPrescribingInformation 
                && !_context.Classifications.Any(c => c.IdNamedEntity == en.Id && c.IdUser == idUser))
            .OrderBy(en => en.Name)
            .ToListAsync(cancellationToken);
    }

}
