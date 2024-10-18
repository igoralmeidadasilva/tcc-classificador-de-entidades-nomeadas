using Classificador.Api.Domain.Models;

namespace Classificador.Api.Domain.Core.Interfaces.Repositories.ReadOnly;

public interface IClassificationReadOnlyRepository : IReadOnlyRepository<Classification>
{
    Task<IEnumerable<CountVoteForNamedEntity>> GetCountingVotesForNamedEntityAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CountVoteForNamedEntity>> GetMostVotedEntityByPrescribingInformation(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Classification>> GetPendingClassificationsByPrescribingInformationAndIdUser(
        Guid idPrescribingInformation,
        Guid idUser,
        CancellationToken cancellationToken = default);

    Task<int> GetCountClassificationByUserId(Guid idUser, Guid idPrescribingInformation, CancellationToken cancellationToken = default);

    Task<int> GetCountClassification(Guid idPrescribingInformation, CancellationToken cancellationToken = default);

    Task<bool> VerifyIfClassificationExistsAsync(Guid idNamedEntity, Guid idUser, CancellationToken cancellationToken = default);
}
