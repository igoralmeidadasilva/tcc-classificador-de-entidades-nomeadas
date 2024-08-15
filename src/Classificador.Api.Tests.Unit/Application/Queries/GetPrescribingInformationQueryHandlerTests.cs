namespace Classificador.Api.Tests.Unit.Application.Queries;

public sealed class GetPrescribingInformationQueryHandlerTests
{
    private readonly Mock<IPrescribingInformationReadOnlyRepository> _prescribingInformationReadOnlyRepositoryMock;
    private readonly Mock<ILogger<GetPrescribingInformationQueryHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetPrescribingInformationQueryHandler _handler;

    public GetPrescribingInformationQueryHandlerTests()
    {
        _prescribingInformationReadOnlyRepositoryMock = new Mock<IPrescribingInformationReadOnlyRepository>();
        _loggerMock = new Mock<ILogger<GetPrescribingInformationQueryHandler>>();
        _mapperMock = new Mock<IMapper>();

        _handler = new GetPrescribingInformationQueryHandler(
            _loggerMock.Object, 
            _mapperMock.Object, 
            _prescribingInformationReadOnlyRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_EmptyPrescribingInformationsList_ShouldReturnFailure()
    {
        // Arrange
        _prescribingInformationReadOnlyRepositoryMock
            .Setup(repo => repo.GetByNameOrDescriptionAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Enumerable.Empty<PrescribingInformation>());

        var query = new GetPrescribingInformationQuery { PrescribingInformationName = "Info" };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);
        var valueResult = result as Result<IEnumerable<ChoosePrescribingInformationViewDto>>;

        // Assert
        Assert.True(valueResult!.IsSuccess);
        Assert.NotNull(valueResult!.Value);
        Assert.Empty(valueResult!.Value);
    }

    [Fact]
    public async Task Handle_PrescribingInformationsFound_ShouldReturnSuccess()
    {
        // Arrange
        var prescribingInformations = new List<PrescribingInformation>
        {
            new("Info1", "Text", string.Empty),
            new("Info2", "Text", string.Empty),
        };

        _prescribingInformationReadOnlyRepositoryMock
            .Setup(repo => repo.GetByNameOrDescriptionAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(prescribingInformations);

        _mapperMock
            .Setup(mapper => mapper.Map<ChoosePrescribingInformationViewDto>(It.IsAny<PrescribingInformation>()))
            .Returns((PrescribingInformation src) => new ChoosePrescribingInformationViewDto { Id = src.Id, Name = src.Name });

        var query = new GetPrescribingInformationQuery { PrescribingInformationName = "Info" };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);
        var valueResult = result as Result<IEnumerable<ChoosePrescribingInformationViewDto>>;

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(valueResult!.Value);
        Assert.Equal(2, valueResult.Value.Count());
    }
}
