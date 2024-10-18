using Classificador.Api.Domain.Models;

namespace Classificador.Api.Application.Queries.GetAllClassificationByVotes;

public sealed record GetAllClassificationByVotesQueryResponse : IQueryResponse
{
    public IEnumerable<CountVoteForNamedEntity> Response { get; init; } = [];
}
