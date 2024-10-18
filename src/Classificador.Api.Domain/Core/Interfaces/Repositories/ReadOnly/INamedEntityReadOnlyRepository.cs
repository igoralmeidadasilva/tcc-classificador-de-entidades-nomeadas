namespace Classificador.Api.Domain.Core.Interfaces.Repositories.ReadOnly;

public interface INamedEntityReadOnlyRepository : IReadOnlyRepository<NamedEntity>
{
    Task<IEnumerable<NamedEntity>> GetByPrescribingInformationAndUserAsync(
        Guid idPrescribingInformation,
        Guid idUser,
        CancellationToken cancellationToke = default);
    Task<IEnumerable<NamedEntity>> GetByPrescribingInformationAsync(Guid idPrescribingInformation, CancellationToken cancellationToken = default);
}
