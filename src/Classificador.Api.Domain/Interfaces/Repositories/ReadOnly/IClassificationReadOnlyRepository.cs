namespace Classificador.Api.Domain.Interfaces.Repositories.ReadOnly;

public interface IClassificationReadOnlyRepository : IReadOnlyRepository<Classification>
{
    Task<IEnumerable<CountVoteForNamedEntity>> GetCountingVotesForNamedEntityAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CountVoteForNamedEntity>> GetMostVotedEntityByPrescribingInformation(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Classification>> GetPendingClassificationsByPrescribingInformationAndIdUser(
        Guid idPrescribingInformation, 
        Guid idUser, 
        CancellationToken cancellationToken = default);
}
