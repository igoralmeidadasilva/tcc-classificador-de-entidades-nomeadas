namespace Classificador.Api.Domain.Interfaces.Repositories.ReadOnly;

public interface INamedEntityReadOnlyRepository : IReadOnlyRepository<NamedEntity>
{
    Task<IEnumerable<NamedEntity>> GetByPrescribingInformationIdAsync(Guid idPrescribingInformation, Guid idUser, CancellationToken cancellationToken);
}
