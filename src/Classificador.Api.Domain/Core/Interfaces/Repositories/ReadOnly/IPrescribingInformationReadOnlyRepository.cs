namespace Classificador.Api.Domain.Core.Interfaces.Repositories.ReadOnly;

public interface IPrescribingInformationReadOnlyRepository : IReadOnlyRepository<PrescribingInformation>
{
    Task<IEnumerable<PrescribingInformation>> GetByNameOrDescriptionAsync(string name, CancellationToken cancellationToken = default);
}
