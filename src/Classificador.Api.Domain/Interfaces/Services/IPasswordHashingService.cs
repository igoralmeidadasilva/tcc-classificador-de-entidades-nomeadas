namespace Classificador.Api.Domain.Interfaces.Services;

public interface IPasswordHashingService
{
    string HashPasswordAsync(string password);
    bool VerifyPasswordAsync(string hashedPassword, string providedPassword);
}
