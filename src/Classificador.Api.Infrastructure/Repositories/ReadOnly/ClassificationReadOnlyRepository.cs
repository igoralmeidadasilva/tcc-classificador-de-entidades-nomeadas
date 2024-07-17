namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public class ClassificationReadOnlyRepository : BaseReadOnlyRepository<Classification>, IClassificationReadOnlyRepository
{
    public ClassificationReadOnlyRepository(ClassifierContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CountVoteForNamedEntity>> GetCountingVotesForNamedEntityAsync(Guid id, CancellationToken cancellationToken = default)
    {   
        var query = await _context.Classifications
            .Where(cla => cla.IdNamedEntity == id)
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
            }).ToListAsync();
        
        return query;
    } 
}

