namespace Classificador.Api.Application.Dtos;

public sealed record ClassifyNamedEntityViewNamedEntityDto
{
    public Guid Id { get; init; } = Guid.Empty;
    public string Name { get; init; } = string.Empty;
}