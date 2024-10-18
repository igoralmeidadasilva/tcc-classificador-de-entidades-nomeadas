using Classificador.Api.Domain.Core.Errors;

namespace Classificador.Api.Tests.Unit.Application.Queries.GetAllClassificationByVotes;

public sealed class GetAllClassificationByVotesQueryHandlerTests
{
    private readonly Mock<ILogger<GetAllClassificationByVotesQueryHandler>> _loggerMock;
    private readonly Mock<IClassificationReadOnlyRepository> _classificationReadOnlyRepoMock;
    private readonly Mock<IPrescribingInformationReadOnlyRepository> _prescribingInformationReadOnlyRepoMock;
    private readonly GetAllClassificationByVotesQueryHandler _handler;

    public GetAllClassificationByVotesQueryHandlerTests()
    {
        _loggerMock = new Mock<ILogger<GetAllClassificationByVotesQueryHandler>>();
        _classificationReadOnlyRepoMock = new Mock<IClassificationReadOnlyRepository>();
        _prescribingInformationReadOnlyRepoMock = new Mock<IPrescribingInformationReadOnlyRepository>();

        _handler = new GetAllClassificationByVotesQueryHandler(
            _loggerMock.Object,
            _classificationReadOnlyRepoMock.Object,
            _prescribingInformationReadOnlyRepoMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenPrescribingInformationDoesNotExist()
    {
        // Arrange
        var query = new GetAllClassificationByVotesQuery(Guid.NewGuid().ToString());

        _classificationReadOnlyRepoMock.Setup(repo => repo.ExistsAsync(query.IdPrescribingInformation, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.PrescribingInformation.PrescribingInformationEntityNotFound, result.Error);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenPrescribingInformationExists()
    {
        // Arrange
        var query = new GetAllClassificationByVotesQuery(Guid.NewGuid().ToString());

        var counts = new List<CountVoteForNamedEntity>
        {
            new() { Entity = "Entity-1", Category = "Category", Votes = 10 },
            new() { Entity = "Entity-2", Category = "Category", Votes = 5 },
        };

        _classificationReadOnlyRepoMock.Setup(repo => repo.ExistsAsync(query.IdPrescribingInformation, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _classificationReadOnlyRepoMock.Setup(repo => repo.GetMostVotedEntityByPrescribingInformation(
                query.IdPrescribingInformation, It.IsAny<CancellationToken>()))
            .ReturnsAsync(counts);
        _prescribingInformationReadOnlyRepoMock.Setup(repo => repo.ExistsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);
        var valueResult = result as Result<List<CountVoteForNamedEntity>>;

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(counts, valueResult!.Value);
    }
}
