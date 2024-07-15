namespace Classificador.Api.Tests.Unit.Application.Commands.CreateUser;

public sealed class CreateUserCommandHandlerTests
{
    private readonly Mock<ILogger<CreateUserCommandHandler>> _loggerMock;
    private readonly Mock<IUserPersistenceRepository> _userPersistenceRepositoryMock;
    private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepositoryMock;
    private readonly Mock<IPasswordHashingService> _passwordHashingServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<CreateUserCommandHandler>>();
        _userPersistenceRepositoryMock = new Mock<IUserPersistenceRepository>();
        _userReadOnlyRepositoryMock = new Mock<IUserReadOnlyRepository>();
        _passwordHashingServiceMock = new Mock<IPasswordHashingService>();
        _mapperMock = new Mock<IMapper>();

        _handler = new CreateUserCommandHandler(
            _loggerMock.Object,
            _userPersistenceRepositoryMock.Object,
            _userReadOnlyRepositoryMock.Object,
            _passwordHashingServiceMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_Email_Already_Exists()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Email = "test@example.com",
            Password = "Password1@",
            ConfirmPassword = "Password1@",
            Name = "Test User",
            Contact = ""
        };
        _userReadOnlyRepositoryMock.Setup(x => x.IsEmailAlreadyExists(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.User.EmailAlreadyExists, result.Errors.First());
    }

    [Fact]
    public async Task Handle_Should_Return_Success_When_User_Is_Created_Successfully()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Email = "test@example.com",
            Password = "Password1@",
            ConfirmPassword = "Password1@",
            Name = "Test User",
            Contact = ""
        };

        User user = new(command.Email, command.Password, command.Name, UserRole.Padrao, Guid.NewGuid(), command.Contact);

        _userReadOnlyRepositoryMock.Setup(x => x.IsEmailAlreadyExists(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(false);
        _passwordHashingServiceMock.Setup(x => x.HashPassword(It.IsAny<string>()))
                                   .Returns("hashedpassword");
        _mapperMock.Setup(x => x.Map<User>(It.IsAny<CreateUserCommand>()))
                   .Returns(user);
        _userPersistenceRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                                      .ReturnsAsync(user.Id);

        // Act
        Result<Guid>? result = await _handler.Handle(command, CancellationToken.None) as Result<Guid>;

        // Assert
        Assert.True(result!.IsSuccess);
        Assert.Equal(user.Id, result.Value);
    }

    [Fact]
    public async Task Handle_Should_Call_Repositories_And_Services_Correctly()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Email = "test@example.com",
            Password = "Password1@",
            ConfirmPassword = "Password1@",
            Name = "Test User",
            Contact = ""
        };

        User user = new(command.Email, command.Password, command.Name, UserRole.Padrao, Guid.NewGuid(), command.Contact);

        _userReadOnlyRepositoryMock.Setup(x => x.IsEmailAlreadyExists(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _passwordHashingServiceMock.Setup(x => x.HashPassword(It.IsAny<string>()))
            .Returns("hashedpassword");

        _mapperMock.Setup(x => x.Map<User>(It.IsAny<CreateUserCommand>()))
            .Returns(user);
            
        _userPersistenceRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(user.Id);

        // Act
        Result<Guid>? result = await _handler.Handle(command, CancellationToken.None) as Result<Guid>;

        // Assert
        _userReadOnlyRepositoryMock.Verify(x => x.IsEmailAlreadyExists(command.Email, CancellationToken.None), Times.Once);
        _passwordHashingServiceMock.Verify(x => x.HashPassword(command.Password), Times.Once);
        _mapperMock.Verify(x => x.Map<User>(It.Is<CreateUserCommand>(c => c.Password == "hashedpassword")), Times.Once);
        _userPersistenceRepositoryMock.Verify(x => x.AddAsync(It.Is<User>(u => u.Id == result!.Value), CancellationToken.None), Times.Once);
    }
}
