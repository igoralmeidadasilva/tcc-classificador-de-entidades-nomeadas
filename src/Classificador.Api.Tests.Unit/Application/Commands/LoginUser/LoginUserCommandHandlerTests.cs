namespace Classificador.Api.Tests.Unit.Application.Commands.LoginUser;

public sealed class LoginUserCommandHandlerTests
{
    private readonly Mock<ILogger<LoginUserCommandHandler>> _loggerMock;
    private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepositoryMock;
    private readonly Mock<IPasswordHashingService> _passwordHashingServiceMock;
    private readonly Mock<IJwtSecurityTokenService> _tokenServiceMock;
    private readonly LoginUserCommandHandler _handler;

    public LoginUserCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<LoginUserCommandHandler>>();
        _userReadOnlyRepositoryMock = new Mock<IUserReadOnlyRepository>();
        _passwordHashingServiceMock = new Mock<IPasswordHashingService>();
        _tokenServiceMock = new Mock<IJwtSecurityTokenService>();

        _handler = new LoginUserCommandHandler(
            _loggerMock.Object,
            _userReadOnlyRepositoryMock.Object,
            _passwordHashingServiceMock.Object,
            _tokenServiceMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserNotFound()
    {
        // Arrange
        var command = new LoginUserCommand 
        { 
            Email = "nonexistent@example.com", 
            Password = "password123" 
        };

        _userReadOnlyRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.User.UserNotFound, result.Error);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPasswordIsIncorrect()
    {
        // Arrange
        User user = new("test@example.com", "Password1@", "Test User", UserRole.Padrao, Guid.NewGuid(), string.Empty);
        var command = new LoginUserCommand
        {
            Email = user.Email,
            Password = "wrongpassword"
        };
        
        _userReadOnlyRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        _passwordHashingServiceMock.Setup(service => service.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.User.AuthenticationPasswordFailed, result.Error);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenLoginIsSuccessful()
    {
        // Arrange
        User user = new("test@example.com", "Password1@", "Test User", UserRole.Padrao, Guid.NewGuid(), string.Empty);
        var command = new LoginUserCommand
        {
            Email = user.Email,
            Password = "correctpassword"
        };
        var claims = new List<Claim> 
        { 
            new(ClaimTypes.Email, user.Email) 
        };
        var token = new JwtToken
        {
            Token = "token",
            ExpiredAtOnUtc = DateTime.UtcNow.AddMinutes(30)
        };

        _userReadOnlyRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        _passwordHashingServiceMock.Setup(service => service.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);
        _tokenServiceMock.Setup(service => service.GenerateClaims(It.IsAny<User>())).Returns(claims);
        _tokenServiceMock.Setup(service => service.GenerateToken(It.IsAny<IEnumerable<Claim>>())).Returns(token);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        var valueResult = result as Result<JwtToken>;

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(token, valueResult!.Value);
    }
}
