namespace Classificador.Api.Domain.Core.Interfaces.Repositories.ReadOnly;

public interface ICategoryReadOnlyRepository : IReadOnlyRepository<Category>
{
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
}
