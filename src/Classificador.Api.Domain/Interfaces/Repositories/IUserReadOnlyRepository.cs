namespace Classificador.Api.Domain.Interfaces.Repositories;

public interface IUserReadOnlyRepository : IReadOnlyRepository<User>
{
    Task<bool> IsEmailAlreadyExists(string email, CancellationToken cancellationToken = default);
}
