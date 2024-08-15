namespace Classificador.Api.Application.Dtos;

public sealed record ClassifyNamedEntityViewCategoryDto
{
    public string? Name { get; init; }

    public override string ToString()
    {
        return $"{Name}";
    }
}