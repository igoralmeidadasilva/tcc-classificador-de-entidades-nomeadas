namespace Classificador.Api.Application.Queries.CountingVotesForNamedEntity;

public sealed record CountingVotesForNamedEntityQuery : IQuery<Result>
{
    public Guid IdNamedEntity { get; init; }
}
