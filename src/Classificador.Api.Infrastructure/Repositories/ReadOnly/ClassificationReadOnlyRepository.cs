namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public class ClassificationReadOnlyRepository : BaseReadOnlyRepository<Classification>, IClassificationReadOnlyRepository
{
    public ClassificationReadOnlyRepository(ClassifierContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CountVoteForNamedEntity>> GetCountingVotesForNamedEntityAsync(Guid id, CancellationToken cancellationToken = default)
    {   
        return await _context.Classifications
            .Where(cla => cla.IdNamedEntity == id)
            .Where(cla => cla.Status == ClassificationStatus.Completo)
            .AsNoTracking()
            .GroupBy(cla => new 
            { 
                CategoryName = cla.Category!.Name,
                NamedEntityName = cla.NamedEntity!.Name,
                CategoryId = cla.Category!.Id 
            })
            .Select(group => new CountVoteForNamedEntity 
            {
                Votes = group.Count(),
                Entity = group.Key.NamedEntityName,
                Category = group.Key.CategoryName
            })
            .OrderByDescending(x => x.Votes)
            .ToListAsync(cancellationToken);         
    }

    public async Task<IEnumerable<Classification>> GetPendingClassificationsByPrescribingInformationAndIdUser(
        Guid idPrescribingInformation, 
        Guid idUser, 
        CancellationToken cancellationToken = default)
    {
        var query = await _context.Classifications
            .AsNoTracking()
            .Include(cla => cla.NamedEntity)
            .Include(cla => cla.Category)
            .Where(cla => cla.IdUser.Equals(idUser))
            .Where(cla => cla.Status.Equals(ClassificationStatus.Pendente))
            .Where(cla => cla.NamedEntity!.IdPrescribingInformation.Equals(idPrescribingInformation))
            .ToListAsync(cancellationToken);
        return query;
        
    }
}

