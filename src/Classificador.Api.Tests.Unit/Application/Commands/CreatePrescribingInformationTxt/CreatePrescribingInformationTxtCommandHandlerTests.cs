namespace Classificador.Api.Tests.Unit.Application.Commands.CreatePrescribingInformationTxt;

public sealed class CreatePrescribingInformationTxtCommandHandlerTests
{
    private readonly Mock<ILogger<CreatePrescribingInformationTxtCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IPrescribingInformationPersistenceRepository> _prescribingInformationPersistenceRepositoryMock;
    private readonly CreatePrescribingInformationTxtCommandHandler _handler;

    public CreatePrescribingInformationTxtCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<CreatePrescribingInformationTxtCommandHandler>>();  
        _mapperMock = new Mock<IMapper>();
        _prescribingInformationPersistenceRepositoryMock = new Mock<IPrescribingInformationPersistenceRepository>();
        
        _handler = new CreatePrescribingInformationTxtCommandHandler
        (
            _loggerMock.Object,
            _mapperMock.Object,
            _prescribingInformationPersistenceRepositoryMock.Object
        );
    }

    [Fact]
    public async Task Handle_Should_Execute_Correctly_And_Return_Success()
    {
        // Arrange
        var fileMock = new Mock<IFormFile>();
        fileMock.Setup(f => f.ContentType).Returns("text/plain");
        fileMock.Setup(f => f.FileName).Returns("test.txt");

        var prescribingInformation = new PrescribingInformation("TestPrescribingInformation", "This is a test text", string.Empty);

        _mapperMock.Setup(x => x.Map<PrescribingInformation>(It.IsAny<CreatePrescribingInformationTxtCommand>()))
            .Returns(prescribingInformation);

        var command = new CreatePrescribingInformationTxtCommand 
        { 
            File = fileMock.Object 
        };
        var expectedId = Guid.NewGuid();

        _prescribingInformationPersistenceRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<PrescribingInformation>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None) as Result<Guid>;;

        // Assert
        Assert.True(result!.IsSuccess);
        Assert.Equal(expectedId, result.Value);
    }

}
