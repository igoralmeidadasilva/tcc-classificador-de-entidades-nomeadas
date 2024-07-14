namespace Classificador.Api.Tests.Unit.Infrastructure.Services;

public sealed class PasswordHashingServiceTests
{
    private readonly IPasswordHashingService _passwordHashingService;

    public PasswordHashingServiceTests()
    {
        _passwordHashingService = new PasswordHashingService();
    }

    [Fact]
    public void HashPassword_ShouldReturnHashedPassword()
    {
        // Arrange
        var password = "TestPassword123";

        // Act
        var hashedPassword = _passwordHashingService.HashPassword(password);

        // Assert
        Assert.False(string.IsNullOrEmpty(hashedPassword));
        Assert.NotEqual(password, hashedPassword);
    }

    [Fact]
    public void HashPassword_ShouldThrowArgumentException_WhenPasswordIsNull()
    {
        // Arrange
        string password = null!;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => _passwordHashingService.HashPassword(password));
        Assert.Equal("password", ex.ParamName);
    }

    [Fact]
    public void HashPassword_ShouldThrowArgumentException_WhenPasswordIsWhitespace()
    {
        // Arrange
        string password = "   ";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _passwordHashingService.HashPassword(password));
        Assert.Equal("password", ex.ParamName);
    }

    [Fact]
    public void VerifyPassword_ShouldReturnTrue_WhenPasswordsMatch()
    {
        // Arrange
        var password = "TestPassword123";
        var hashedPassword = _passwordHashingService.HashPassword(password);

        // Act
        var result = _passwordHashingService.VerifyPassword(hashedPassword, password);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void VerifyPassword_ShouldReturnFalse_WhenPasswordsDoNotMatch()
    {
        // Arrange
        var password = "TestPassword123";
        var hashedPassword = _passwordHashingService.HashPassword(password);
        var wrongPassword = "WrongPassword456";

        // Act
        var result = _passwordHashingService.VerifyPassword(hashedPassword, wrongPassword);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void VerifyPassword_ShouldThrowArgumentException_WhenHashedPasswordIsNull()
    {
        // Arrange
        string hashedPassword = null!;
        var providedPassword = "TestPassword123";

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => _passwordHashingService.VerifyPassword(hashedPassword, providedPassword));
        Assert.Equal("hashedPassword", ex.ParamName);
    }

    [Fact]
    public void VerifyPassword_ShouldThrowArgumentException_WhenProvidedPasswordIsNull()
    {
        // Arrange
        var hashedPassword = _passwordHashingService.HashPassword("TestPassword123");
        string providedPassword = null!;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => _passwordHashingService.VerifyPassword(hashedPassword, providedPassword));
        Assert.Equal("providedPassword", ex.ParamName);
    }
}
