namespace Classificador.Api.Presentation.Models;
public sealed record ChoosePrescribingInformationViewModel
{
    public string? PrescribingInformationName { get; set; } = string.Empty; 
    public List<ChoosePrescribingInformationViewDto>? PrescribingInformations { get; set; }
}