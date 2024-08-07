namespace Classificador.Api.Domain.Interfaces.Repositories.ReadOnly;

public interface IClassificationReadOnlyRepository : IReadOnlyRepository<Classification>
{
    Task<IEnumerable<CountVoteForNamedEntity>> GetCountingVotesForNamedEntityAsync(Guid id, CancellationToken cancellationToken = default);
}
