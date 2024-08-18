namespace Classificador.Api.Application.Dtos;

public sealed record ClassifyNamedEntityViewNamedEntityDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
}