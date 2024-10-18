namespace Classificador.Api.Domain.Core.Interfaces.Repositories.ReadOnly;

public interface ISpecialtyReadOnlyRepository : IReadOnlyRepository<Specialty>
{
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
}