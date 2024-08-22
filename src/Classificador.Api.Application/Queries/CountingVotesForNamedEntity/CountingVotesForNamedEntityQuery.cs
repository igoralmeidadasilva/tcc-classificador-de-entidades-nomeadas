namespace Classificador.Api.Application.Queries.CountingVotesForNamedEntity;

[Obsolete]
public sealed record CountingVotesForNamedEntityQuery : IQuery<Result>
{
    public Guid IdNamedEntity { get; init; }
}
