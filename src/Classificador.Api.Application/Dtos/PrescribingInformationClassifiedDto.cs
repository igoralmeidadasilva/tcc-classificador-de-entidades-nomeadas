namespace Classificador.Api.Application.Dtos;

public sealed record PrescribingInformationClassifiedDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public int Amount { get; init; }
    public string? Description { get; init; }
}