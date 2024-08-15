namespace Classificador.Api.Domain.Interfaces.Repositories.ReadOnly;

public interface IPrescribingInformationReadOnlyRepository : IReadOnlyRepository<PrescribingInformation>
{
    Task<IEnumerable<PrescribingInformation>> GetByNameOrDescriptionAsync(string name, CancellationToken cancellationToken = default);
}
