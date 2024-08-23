namespace Classificador.Api.Application.Dtos;

public sealed record ChoosePrescribingInformationViewDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public int Amount { get; init; }
    public int UserAmount { get; init; }
}