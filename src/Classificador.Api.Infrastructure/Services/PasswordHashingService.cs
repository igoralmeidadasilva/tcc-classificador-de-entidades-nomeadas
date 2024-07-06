namespace Classificador.Api.Infrastructure.Services;

public sealed class PasswordHashingService : IPasswordHashingService
{
    public string HashPasswordAsync(string password)
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(password, nameof(password));

        string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

        return hashedPassword;
    }

    public bool VerifyPasswordAsync(string hashedPassword, string providedPassword)
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(hashedPassword, nameof(hashedPassword));
        ArgumentValidator.ThrowIfNullOrWhitespace(providedPassword, nameof(providedPassword));

        return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }

}
