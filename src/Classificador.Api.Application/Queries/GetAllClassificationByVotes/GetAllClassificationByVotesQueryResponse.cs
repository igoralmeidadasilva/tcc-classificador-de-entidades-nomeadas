using Classificador.Api.Domain.Models;

namespace Classificador.Api.Application.Queries.GetAllClassificationByVotes;

public sealed record GetAllClassificationByVotesQueryResponse : IQueryResponse
{
    public string? Name { get; init;}
    public IEnumerable<CountVoteForNamedEntity>? Classifications { get; init; }
}
