using Classificador.Api.Domain.Core.Errors;

namespace Classificador.Api.Tests.Unit.Application.Commands.UpdateClassificationToCompleted;

public sealed class UpdateClassificationToCompletedCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateClassificationToCompletedCommandHandler>> _loggerMock;
    private readonly Mock<IClassificationPersistenceRepository> _classificationPersistenceRepoMock;
    private readonly Mock<IClassificationReadOnlyRepository> _classificationReadOnlyRepoMock;
    private readonly UpdateClassificationToCompletedCommandHandler _handler;

    public UpdateClassificationToCompletedCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateClassificationToCompletedCommandHandler>>();
        _classificationPersistenceRepoMock = new Mock<IClassificationPersistenceRepository>();
        _classificationReadOnlyRepoMock = new Mock<IClassificationReadOnlyRepository>();

        _handler = new UpdateClassificationToCompletedCommandHandler(
            _loggerMock.Object,
            _classificationPersistenceRepoMock.Object,
            _classificationReadOnlyRepoMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenNoPendingClassificationsFound()
    {
        // Arrange
        var command = new UpdateClassificationToCompletedCommand
        {
            IdPrescribingInformation = Guid.NewGuid(),
            IdUser = Guid.NewGuid()
        };

        _classificationReadOnlyRepoMock.Setup(repo => repo.GetPendingClassificationsByPrescribingInformationAndIdUser(
                command.IdPrescribingInformation, command.IdUser, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Classification>());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.Classification.ClassificationsPendingNotFound, result.Error);
    }

    [Fact]
    public async Task Handle_ShouldUpdateClassificationsToCompleted_WhenPendingClassificationsExist()
    {
        // Arrange
        var command = new UpdateClassificationToCompletedCommand
        {
            IdPrescribingInformation = Guid.NewGuid(),
            IdUser = Guid.NewGuid()
        };

        var classifications = new List<Classification>
        {
            new() { Id = Guid.NewGuid() },
            new() { Id = Guid.NewGuid() }
        };

        _classificationReadOnlyRepoMock.Setup(repo => repo.GetPendingClassificationsByPrescribingInformationAndIdUser(
                command.IdPrescribingInformation, command.IdUser, It.IsAny<CancellationToken>()))
            .ReturnsAsync(classifications);

        _classificationPersistenceRepoMock.Setup(repo => repo.UpdateStatusToCompletedAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        _classificationPersistenceRepoMock.Verify(repo => repo.UpdateStatusToCompletedAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Exactly(classifications.Count));
    }
}
