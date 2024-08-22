namespace Classificador.Api.Tests.Unit.Application.Queries.CoutingVotesForNamedEntity;

public class CountingVotesForNamedEntityQueryHandlerTests
{
    private readonly Mock<ILogger<CountingVotesForNamedEntityQueryHandler>> _loggerMock;
    private readonly Mock<IClassificationReadOnlyRepository> _classificationReadOnlyRepositoryMock;
    private readonly Mock<INamedEntityReadOnlyRepository> _namedEntityReadOnlyRepositoryMock;
    private readonly CountingVotesForNamedEntityQueryHandler _handler;

    public CountingVotesForNamedEntityQueryHandlerTests()
    {
        _loggerMock = new Mock<ILogger<CountingVotesForNamedEntityQueryHandler>>();
        _classificationReadOnlyRepositoryMock = new Mock<IClassificationReadOnlyRepository>();
        _namedEntityReadOnlyRepositoryMock = new Mock<INamedEntityReadOnlyRepository>();
        _handler = new CountingVotesForNamedEntityQueryHandler(
            _loggerMock.Object,
            _classificationReadOnlyRepositoryMock.Object,
            _namedEntityReadOnlyRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_NamedEntityDoesNotExist_ReturnsFailureResult()
    {
        // Arrange
        var request = new CountingVotesForNamedEntityQuery
        {
            IdNamedEntity = Guid.NewGuid()
        };

        _namedEntityReadOnlyRepositoryMock.Setup(repo => repo.ExistsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.NamedEntity.NamedEntityNotFound, result.Error);
    }
    [Fact]
    public async Task Handle_NamedEntityExists_ReturnsSuccessResultWithVotes()
    {
        // Arrange
        var request = new CountingVotesForNamedEntityQuery
        {
            IdNamedEntity = Guid.NewGuid()
        };

        _namedEntityReadOnlyRepositoryMock.Setup(repo => repo.ExistsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var expectedVotes = new List<CountVoteForNamedEntity>
        {
            new CountVoteForNamedEntity(),
            new CountVoteForNamedEntity()
        };

        _classificationReadOnlyRepositoryMock.Setup(repo => repo.GetCountingVotesForNamedEntityAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedVotes);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        var resultValue = result as Result<IEnumerable<CountVoteForNamedEntity>>;

        // Assert
        Assert.True(resultValue!.IsSuccess);
        Assert.Equal(expectedVotes, resultValue.Value!);
    }

    [Fact]
    public async Task Handle_NamedEntityExists_NoVotes_ReturnsSuccessResultWithEmptyList()
    {
        // Arrange
        var request = new CountingVotesForNamedEntityQuery
        {
            IdNamedEntity = Guid.NewGuid()
        };

        var expectedVotes = new List<CountVoteForNamedEntity>();

        _namedEntityReadOnlyRepositoryMock.Setup(repo => repo.ExistsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _classificationReadOnlyRepositoryMock.Setup(repo => repo.GetCountingVotesForNamedEntityAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedVotes);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);
        var resultValue = result as Result<IEnumerable<CountVoteForNamedEntity>>;

        // Assert
        Assert.True(resultValue!.IsSuccess);
        Assert.Empty(resultValue.Value!);
    }
}

