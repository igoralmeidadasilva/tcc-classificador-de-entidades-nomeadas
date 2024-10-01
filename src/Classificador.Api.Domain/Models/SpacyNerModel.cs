namespace Classificador.Api.Domain.Models;

public sealed record SpacyNerModel
{
    public int Start { get; set; }
    public int End { get; set; }
    public string? Label { get; set; }
}
