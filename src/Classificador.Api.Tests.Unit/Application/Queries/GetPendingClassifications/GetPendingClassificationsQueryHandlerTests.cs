using Classificador.Api.Domain.Core.Errors;
using Classificador.Api.Domain.Core.Interfaces.Repositories.ReadOnly;

namespace Classificador.Api.Tests.Unit.Application.Queries.GetPendingClassifications;

public sealed class GetPendingClassificationsQueryHandlerTests
{
    private readonly Mock<ILogger<GetPendingClassificationsQueryHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IClassificationReadOnlyRepository> _classificationReadOnlyRepositoryMock;
    private readonly GetPendingClassificationsQueryHandler _handler;

    public GetPendingClassificationsQueryHandlerTests()
    {
        _loggerMock = new Mock<ILogger<GetPendingClassificationsQueryHandler>>();
        _mapperMock = new Mock<IMapper>();
        _classificationReadOnlyRepositoryMock = new Mock<IClassificationReadOnlyRepository>();
        _handler = new GetPendingClassificationsQueryHandler(_loggerMock.Object, _mapperMock.Object, _classificationReadOnlyRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_NoClassificationsFound_ShouldReturnFailure()
    {
        // Arrange
        string idPrescribingInformation = Guid.NewGuid().ToString();
        string idUser = Guid.NewGuid().ToString();

        var query = new GetPendingClassificationsQuery(idUser, idPrescribingInformation);

        _classificationReadOnlyRepositoryMock
            .Setup(x => x.GetPendingClassificationsByPrescribingInformationAndIdUser(query.IdPrescribingInformation, query.IdUser, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IEnumerable<Classification>)null!);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.NamedEntity.NamedEntityNoneWereFound.Code, result.Error.Code);
    }

    [Fact]
    public async Task Handle_NoPendingClassifications_ShouldReturnEmptySuccess()
    {
        // Arrange
        string idPrescribingInformation = Guid.NewGuid().ToString();
        string idUser = Guid.NewGuid().ToString();

        var query = new GetPendingClassificationsQuery(idUser, idPrescribingInformation);
        var pendingClassifications = Enumerable.Empty<Classification>();

        _classificationReadOnlyRepositoryMock
            .Setup(x => x.GetPendingClassificationsByPrescribingInformationAndIdUser(query.IdPrescribingInformation, query.IdUser, It.IsAny<CancellationToken>()))
            .ReturnsAsync(pendingClassifications);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);
        var valueResult = result as Result<List<ClassifyNamedEntityViewPendingClassificationDto>>;

        // Assert
        Assert.True(result.IsSuccess);
        var data = valueResult!.Value as List<ClassifyNamedEntityViewPendingClassificationDto>;
        Assert.NotNull(data);
        Assert.Empty(data);
    }

    [Fact]
    public async Task Handle_PendingClassificationsFound_ShouldReturnMappedSuccess()
    {
        // Arrange
        string idPrescribingInformation = Guid.NewGuid().ToString();
        string idUser = Guid.NewGuid().ToString();

        var query = new GetPendingClassificationsQuery(idUser, idPrescribingInformation);
        var pendingClassifications = new List<Classification> 
        { 
            new(), 
            new() 
        };

        var mappedClassifications = new List<ClassifyNamedEntityViewPendingClassificationDto> 
        { 
            new(), 
            new() 
        };

        _classificationReadOnlyRepositoryMock
            .Setup(x => x.GetPendingClassificationsByPrescribingInformationAndIdUser(query.IdPrescribingInformation, query.IdUser, It.IsAny<CancellationToken>()))
            .ReturnsAsync(pendingClassifications);

        _mapperMock
            .Setup(m => m.Map<ClassifyNamedEntityViewPendingClassificationDto>(It.IsAny<Classification>()))
            .Returns(mappedClassifications.First());

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);
        var valueResult = result as Result<List<ClassifyNamedEntityViewPendingClassificationDto>>;

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(valueResult!.Value);
        Assert.Equal(mappedClassifications.Count, valueResult!.Value.Count);

    }
}
