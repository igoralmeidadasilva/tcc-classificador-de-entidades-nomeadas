namespace Classificador.Api.Domain.Interfaces.Repositories.ReadOnly;

public interface IUserReadOnlyRepository : IReadOnlyRepository<User>
{
    Task<bool> IsEmailAlreadyExists(string email, CancellationToken cancellationToken = default);

    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

}
