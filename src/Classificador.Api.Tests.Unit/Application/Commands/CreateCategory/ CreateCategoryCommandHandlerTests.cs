using Classificador.Api.Domain.Core.Errors;
using Classificador.Api.Domain.Core.Interfaces.Repositories.Persistence;
using Classificador.Api.Domain.Core.Interfaces.Repositories.ReadOnly;

namespace Classificador.Api.Tests.Unit.Application.Commands.CreateCategory;

public sealed class CreateCategoryCommandHandlerTests
{
    private readonly Mock<ILogger<CreateCategoryCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ICategoryReadOnlyRepository> _categoryReadOnlyRepositoryMock;
    private readonly Mock<ICategoryPersistenceRepository> _categoryPersistenceRepositoryMock;
    private readonly CreateCategoryCommandHandler _handler;

    public CreateCategoryCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<CreateCategoryCommandHandler>>();
        _mapperMock = new Mock<IMapper>();
        _categoryReadOnlyRepositoryMock = new Mock<ICategoryReadOnlyRepository>();
        _categoryPersistenceRepositoryMock = new Mock<ICategoryPersistenceRepository>();

        _handler = new CreateCategoryCommandHandler(
            _loggerMock.Object,
            _mapperMock.Object,
            _categoryReadOnlyRepositoryMock.Object,
            _categoryPersistenceRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_CategoryNameAlreadyExists_ReturnsFailureResult()
    {
        // Arrange
        var command = new CreateCategoryCommand("Existing Category", string.Empty);
        _categoryReadOnlyRepositoryMock.Setup(repo => repo.ExistsByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.Category.NameAlredyExists, result.Error);
    }

    [Fact]
    public async Task Handle_CategorySuccessfullyCreated_ReturnsSuccessResult()
    {
        // Arrange
        var command = new CreateCategoryCommand("New Category", string.Empty);
        _categoryReadOnlyRepositoryMock.Setup(repo => repo.ExistsByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var category = new Category(command.Name, command.Description);
        _mapperMock.Setup(mapper => mapper.Map<Category>(It.IsAny<CreateCategoryCommand>()))
            .Returns(category);

        _categoryPersistenceRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(category.Id);

        // Act
        Result<Guid>? result = await _handler.Handle(command, CancellationToken.None) as Result<Guid>;

        // Assert
        Assert.True(result!.IsSuccess);
    }
}