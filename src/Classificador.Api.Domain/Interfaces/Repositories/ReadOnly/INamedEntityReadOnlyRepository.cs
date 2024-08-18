namespace Classificador.Api.Domain.Interfaces.Repositories.ReadOnly;

public interface INamedEntityReadOnlyRepository : IReadOnlyRepository<NamedEntity>
{
    Task<IEnumerable<NamedEntity>> GetByPrescribingInformationIdAsync(Guid id, CancellationToken cancellationToken);
}
