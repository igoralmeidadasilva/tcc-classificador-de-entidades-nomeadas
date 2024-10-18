using Classificador.Api.Domain.Core.Errors;
using Classificador.Api.Domain.Core.Interfaces.Repositories.ReadOnly;

namespace Classificador.Api.Tests.Unit.Application.Queries.GetCategories;
public sealed class GetCategoriesQueryHandlerTests
{
    private readonly Mock<ICategoryReadOnlyRepository> _categoryReadOnlyRepositoryMock;
    private readonly Mock<ILogger<GetCategoriesQueryHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetCategoriesQueryHandler _handler;

    public GetCategoriesQueryHandlerTests()
    {
        _categoryReadOnlyRepositoryMock = new Mock<ICategoryReadOnlyRepository>();
        _loggerMock = new Mock<ILogger<GetCategoriesQueryHandler>>();
        _mapperMock = new Mock<IMapper>();

        _handler = new GetCategoriesQueryHandler(
            _loggerMock.Object,
            _mapperMock.Object,
            _categoryReadOnlyRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_NoCategoriesFound_ShouldReturnFailure()
    {
        // Arrange
        _categoryReadOnlyRepositoryMock
            .Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Enumerable.Empty<Category>());

        var query = new GetCategoriesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.Category.CategoryEntityNoneWereFound, result.Error);
    }

    [Fact]
    public async Task Handle_CategoriesFound_ShouldReturnSuccess()
    {
        // Arrange
        var categories = new List<Category>
        {
            new ("Category1", string.Empty),
            new ("Category2", string.Empty),
        };

        _categoryReadOnlyRepositoryMock
            .Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(categories);

        _mapperMock
            .Setup(mapper => mapper.Map<ClassifyNamedEntityViewCategoryDto>(It.IsAny<Category>()))
            .Returns((Category src) => new ClassifyNamedEntityViewCategoryDto
            {
                Name = src.Name
            });

        var query = new GetCategoriesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);
        var valueResult = result as Result<IEnumerable<ClassifyNamedEntityViewCategoryDto>>;

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(valueResult!.Value);
        Assert.Equal(2, valueResult.Value.Count());
    }

    [Fact]
    public async Task Handle_NullCategories_ShouldReturnFailure()
    {
        // Arrange
        _categoryReadOnlyRepositoryMock
            .Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var query = new GetCategoriesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.Category.CategoryEntityNoneWereFound, result.Error);
    }
}
