namespace Classificador.Api.Tests.Unit.Application.Commands.CreateClassification;

public sealed class CreateClassificationCommandHandlerTests
{
    private readonly Mock<ILogger<CreateClassificationCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IUserReadOnlyRepository> _userRepoMock;
    private readonly Mock<INamedEntityReadOnlyRepository> _namedEntityRepoMock;
    private readonly Mock<ICategoryReadOnlyRepository> _categoryRepoMock;
    private readonly Mock<IClassificationPersistenceRepository> _classificationRepoMock;
    private readonly CreateClassificationCommandHandler _handler;

    public CreateClassificationCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<CreateClassificationCommandHandler>>();
        _mapperMock = new Mock<IMapper>();
        _userRepoMock = new Mock<IUserReadOnlyRepository>();
        _namedEntityRepoMock = new Mock<INamedEntityReadOnlyRepository>();
        _categoryRepoMock = new Mock<ICategoryReadOnlyRepository>();
        _classificationRepoMock = new Mock<IClassificationPersistenceRepository>();

        _handler = new CreateClassificationCommandHandler(
            _loggerMock.Object,
            _mapperMock.Object,
            _userRepoMock.Object,
            _namedEntityRepoMock.Object,
            _categoryRepoMock.Object,
            _classificationRepoMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenUserDoesNotExist()
    {
        // Arrange
        var command = new CreateClassificationCommand { IdUser = Guid.NewGuid(), IdNamedEntity = Guid.NewGuid(), IdCategory = Guid.NewGuid() };

        _userRepoMock.Setup(repo => repo.ExistsAsync(command.IdUser, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.User.UserNotFound, result.Error);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenNamedEntityDoesNotExist()
    {
        // Arrange
        var command = new CreateClassificationCommand { IdUser = Guid.NewGuid(), IdNamedEntity = Guid.NewGuid(), IdCategory = Guid.NewGuid() };

        _userRepoMock.Setup(repo => repo.ExistsAsync(command.IdUser, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _namedEntityRepoMock.Setup(repo => repo.ExistsAsync(command.IdNamedEntity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.NamedEntity.NamedEntityNotFound, result.Error);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenCategoryDoesNotExist()
    {
        // Arrange
        var command = new CreateClassificationCommand { IdUser = Guid.NewGuid(), IdNamedEntity = Guid.NewGuid(), IdCategory = Guid.NewGuid() };

        _userRepoMock.Setup(repo => repo.ExistsAsync(command.IdUser, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _namedEntityRepoMock.Setup(repo => repo.ExistsAsync(command.IdNamedEntity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _categoryRepoMock.Setup(repo => repo.ExistsAsync(command.IdCategory, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.Category.CategoryEntityNotFound, result.Error);
    }

    [Fact]
    public async Task Handle_ShouldCreateClassification_WhenAllEntitiesExist()
    {
        // Arrange
        var command = new CreateClassificationCommand { IdUser = Guid.NewGuid(), IdNamedEntity = Guid.NewGuid(), IdCategory = Guid.NewGuid() };

        _userRepoMock.Setup(repo => repo.ExistsAsync(command.IdUser, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _namedEntityRepoMock.Setup(repo => repo.ExistsAsync(command.IdNamedEntity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _categoryRepoMock.Setup(repo => repo.ExistsAsync(command.IdCategory, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var classification = new Classification { Id = Guid.NewGuid() };
        _mapperMock.Setup(mapper => mapper.Map<Classification>(command)).Returns(classification);
        _classificationRepoMock.Setup(repo => repo.AddAsync(It.IsAny<Classification>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(classification.Id);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        var valueResult = result as Result<Guid>;

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(classification.Id, valueResult!.Value);
    }
}
