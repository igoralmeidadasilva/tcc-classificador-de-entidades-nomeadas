using Classificador.Api.Application.Commands.DeletePendingClassification;
using Classificador.Api.Domain.Core.Errors;

namespace Classificador.Api.Tests.Unit.Application.Commands.DeletePendingClassification;

public sealed class DeletePendingClassificationCommandHandlerTests
{
    private readonly Mock<ILogger<DeletePendingClassificationCommandHandler>> _loggerMock;
    private readonly Mock<IClassificationPersistenceRepository> _classificationPersistenceRepoMock;
    private readonly Mock<IClassificationReadOnlyRepository> _classificationReadOnlyRepoMock;
    private readonly DeletePendingClassificationCommandHandler _handler;

    public DeletePendingClassificationCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<DeletePendingClassificationCommandHandler>>();
        _classificationPersistenceRepoMock = new Mock<IClassificationPersistenceRepository>();
        _classificationReadOnlyRepoMock = new Mock<IClassificationReadOnlyRepository>();

        _handler = new DeletePendingClassificationCommandHandler(
            _loggerMock.Object,
            _classificationPersistenceRepoMock.Object,
            _classificationReadOnlyRepoMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenClassificationDoesNotExist()
    {
        // Arrange
        var command = new DeletePendingClassificationCommand { IdClassification = Guid.NewGuid() };

        _classificationReadOnlyRepoMock.Setup(repo => repo.ExistsAsync(command.IdClassification, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.Classification.ClassificationsPendingNotFound, result.Error);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenClassificationExists()
    {
        // Arrange
        var command = new DeletePendingClassificationCommand { IdClassification = Guid.NewGuid() };

        _classificationReadOnlyRepoMock.Setup(repo => repo.ExistsAsync(command.IdClassification, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _classificationPersistenceRepoMock.Setup(repo => repo.DeleteAsync(command.IdClassification, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        _classificationPersistenceRepoMock.Verify(repo => repo.DeleteAsync(command.IdClassification, It.IsAny<CancellationToken>()), Times.Once);
    }
}
