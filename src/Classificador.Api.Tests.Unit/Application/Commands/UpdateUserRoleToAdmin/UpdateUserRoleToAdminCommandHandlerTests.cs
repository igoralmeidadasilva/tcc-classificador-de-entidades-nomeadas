namespace Classificador.Api.Tests.Unit.Application.Commands.UpdateUserRoleToAdmin;

public sealed class UpdateUserRoleToAdminCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateUserRoleToAdminCommandHandler>> _loggerMock;
    private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepositoryMock;
    private readonly Mock<IUserPersistenceRepository> _userPersistenceRepositoryMock;
    private readonly UpdateUserRoleToAdminCommandHandler _handler;

    public UpdateUserRoleToAdminCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateUserRoleToAdminCommandHandler>>();
        _userReadOnlyRepositoryMock = new Mock<IUserReadOnlyRepository>();
        _userPersistenceRepositoryMock = new Mock<IUserPersistenceRepository>();

        _handler = new UpdateUserRoleToAdminCommandHandler(
            _loggerMock.Object,
            _userPersistenceRepositoryMock.Object,
            _userReadOnlyRepositoryMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserNotFound()
    {
        // Arrange
        var command = new UpdateUserRoleToAdminCommand 
        { 
            Id = Guid.NewGuid() 
        };
        
        _userReadOnlyRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.User.UserNotFound, result.Errors.FirstOrDefault());
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenUpdateIsSuccessful()
    {
        // Arrange
        var user = new User("test@example.com", "Password1@", "Test User", UserRole.Padrao, Guid.NewGuid(), string.Empty);
        var command = new UpdateUserRoleToAdminCommand 
        { 
            Id = user.Id 
        };

        _userReadOnlyRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        _userPersistenceRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        _userPersistenceRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<User>(u => u.Role == UserRole.Admin), It.IsAny<CancellationToken>()), Times.Once);
    }
}
