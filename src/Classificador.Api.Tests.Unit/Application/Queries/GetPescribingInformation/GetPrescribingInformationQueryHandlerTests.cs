namespace Classificador.Api.Tests.Unit.Application.Queries.GetPescribingInformation;

public sealed class GetPrescribingInformationQueryHandlerTests
{
    private readonly Mock<ILogger<GetPrescribingInformationByIdQueryHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IPrescribingInformationReadOnlyRepository> _prescribingInformationReadOnlyRepositoryMock;
    private readonly Mock<IClassificationReadOnlyRepository> _classificationReadOnlyRepositoryMock;
    private readonly GetPrescribingInformationByIdQueryHandler _handler;

    public GetPrescribingInformationQueryHandlerTests()
    {
        _loggerMock = new Mock<ILogger<GetPrescribingInformationByIdQueryHandler>>();
        _mapperMock = new Mock<IMapper>();
        _prescribingInformationReadOnlyRepositoryMock = new Mock<IPrescribingInformationReadOnlyRepository>();
        _classificationReadOnlyRepositoryMock = new Mock<IClassificationReadOnlyRepository>();
        _handler = new GetPrescribingInformationByIdQueryHandler(
            _loggerMock.Object,
            _mapperMock.Object,
            _prescribingInformationReadOnlyRepositoryMock.Object,
            _classificationReadOnlyRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_NoPrescribingInformationsFound_ShouldReturnFailure()
    {
        // Arrange
        var query = new GetPrescribingInformationByIdQuery("TestName", Guid.NewGuid().ToString());

        _prescribingInformationReadOnlyRepositoryMock
            .Setup(x => x.GetByNameOrDescriptionAsync(query.PrescribingInformationName!, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IEnumerable<PrescribingInformation>)null!);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.PrescribingInformation.PrescribingInformationEntityNotFound.Code, result.Error.Code);
    }

    [Fact]
    public async Task Handle_PrescribingInformationsFound_ShouldReturnMappedSuccess()
    {
        // Arrange
        var query = new GetPrescribingInformationByIdQuery("TestName", Guid.NewGuid().ToString());

        var prescribingInformations = new List<PrescribingInformation>
        {
            new PrescribingInformation { Id = Guid.NewGuid() },
            new PrescribingInformation { Id = Guid.NewGuid() }
        };

        _prescribingInformationReadOnlyRepositoryMock
            .Setup(x => x.GetByNameOrDescriptionAsync(query.PrescribingInformationName!, It.IsAny<CancellationToken>()))
            .ReturnsAsync(prescribingInformations);

        _mapperMock
            .Setup(m => m.Map<ChoosePrescribingInformationViewDto>(It.IsAny<PrescribingInformation>()))
            .Returns(new ChoosePrescribingInformationViewDto());

        _classificationReadOnlyRepositoryMock
            .Setup(x => x.GetCountClassificationByUserId(query.IdUser, It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(5);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);
        var valueResult = result as Result<List<ChoosePrescribingInformationViewDto>>; 

        // Assert
        Assert.True(result.IsSuccess);
        var data = valueResult!.Value;
        Assert.NotNull(data);
        Assert.Equal(prescribingInformations.Count, data.Count);
    }
}
