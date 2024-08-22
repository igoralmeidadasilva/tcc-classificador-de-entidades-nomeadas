namespace Classificador.Api.Application.Dtos;

public sealed record ClassifyNamedEntityViewPendingClassificationDto
{
    public string? NamedEntity { get; init; }
    public string? Category { get; init; }
    public Guid IdClassification { get; set; }
}