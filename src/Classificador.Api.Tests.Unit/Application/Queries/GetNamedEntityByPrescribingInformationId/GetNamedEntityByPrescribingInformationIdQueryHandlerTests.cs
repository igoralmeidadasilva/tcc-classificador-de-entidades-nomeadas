namespace Classificador.Api.Tests.Unit.Application.Queries.GetNamedEntityByPrescribingInformationId;

public sealed class GetNamedEntityByPrescribingInformationIdQueryHandlerTests
{ 
    private readonly GetNamedEntityByPrescribingInformationIdQueryHandler _handler;
    private readonly Mock<ILogger<GetNamedEntityByPrescribingInformationIdQueryHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<INamedEntityReadOnlyRepository> _namedEntityReadOnlyRepositoryMock;

    public GetNamedEntityByPrescribingInformationIdQueryHandlerTests()
    {
        _loggerMock = new Mock<ILogger<GetNamedEntityByPrescribingInformationIdQueryHandler>>();
        _mapperMock = new Mock<IMapper>();
        _namedEntityReadOnlyRepositoryMock = new Mock<INamedEntityReadOnlyRepository>();

        _handler = new GetNamedEntityByPrescribingInformationIdQueryHandler(
            _loggerMock.Object,
            _mapperMock.Object,
            _namedEntityReadOnlyRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_NamedEntitiesIsNull_ShouldReturnFailureResult()
    {
        // Arrange
        string idPrescribingInformation = Guid.NewGuid().ToString();
        string idUser = Guid.NewGuid().ToString();

        var query = new GetNamedEntityByPrescribingInformationIdQuery(idPrescribingInformation, idUser);

        _namedEntityReadOnlyRepositoryMock
            .Setup(repo => repo.GetByPrescribingInformationAndUserAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((IEnumerable<NamedEntity>)null!);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.NamedEntity.NamedEntityNoneWereFound, result.Error);
    }

    [Fact]
    public async Task Handle_NoNamedEntities_ShouldReturnEmptyList()
    {
        // Arrange
        string idPrescribingInformation = Guid.NewGuid().ToString();
        string idUser = Guid.NewGuid().ToString();

        var query = new GetNamedEntityByPrescribingInformationIdQuery(idPrescribingInformation, idUser);

        _namedEntityReadOnlyRepositoryMock
            .Setup(repo => repo.GetByPrescribingInformationAndUserAsync(query.IdPrescribingInformation, query.IdUser, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<NamedEntity>());

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);
        var valueResult = result as Result<List<ClassifyNamedEntityViewNamedEntityDto>>;

        // Assert
        Assert.True(result.IsSuccess);
        var value = Assert.IsType<List<ClassifyNamedEntityViewNamedEntityDto>>(valueResult!.Value);
        Assert.Empty(value);

    }

    [Fact]
    public async Task Handle_FoundNamedEntities_ShouldReturnMappedEntities()
    {
        // Arrange
        string idPrescribingInformation = Guid.NewGuid().ToString();
        string idUser = Guid.NewGuid().ToString();

        var query = new GetNamedEntityByPrescribingInformationIdQuery(idPrescribingInformation, idUser);

        var namedEntities = new List<NamedEntity>
        {
            new("name-1", string.Empty, WordPosition.Create(0,1)),
            new("name-2", string.Empty, WordPosition.Create(0,1)),
        };

        _namedEntityReadOnlyRepositoryMock
            .Setup(repo => repo.GetByPrescribingInformationAndUserAsync(query.IdPrescribingInformation, query.IdUser, It.IsAny<CancellationToken>()))
            .ReturnsAsync(namedEntities);

        _mapperMock
            .Setup(mapper => mapper.Map<ClassifyNamedEntityViewNamedEntityDto>(It.IsAny<NamedEntity>()))
            .Returns(new ClassifyNamedEntityViewNamedEntityDto());

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);
        var valueResult = result as Result<List<ClassifyNamedEntityViewNamedEntityDto>>;

        // Assert
        Assert.True(result.IsSuccess);
        var value = Assert.IsType<List<ClassifyNamedEntityViewNamedEntityDto>>(valueResult!.Value);
        Assert.Equal(namedEntities.Count, value.Count);

    }
}
