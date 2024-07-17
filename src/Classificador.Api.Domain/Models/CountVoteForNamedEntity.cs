namespace Classificador.Api.Domain.Models;

public sealed record CountVoteForNamedEntity
{
    public int Votes { get; init; }
    public string? Entity { get; init; }
    public string? Category { get; init; }
}
