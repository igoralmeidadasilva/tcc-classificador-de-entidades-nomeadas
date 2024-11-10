namespace Classificador.Api.Presentation.Models;
public sealed record ChoosePrescribingInformationViewModel
{
    public string SearchTerm { get; set; } = string.Empty; 
    public IEnumerable<ChoosePrescribingInformationViewDto>? PrescribingInformations { get; set; }
}