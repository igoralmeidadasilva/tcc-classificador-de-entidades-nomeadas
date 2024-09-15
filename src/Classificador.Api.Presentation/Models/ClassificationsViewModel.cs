namespace Classificador.Api.Presentation.Models;

public sealed record ClassificationsViewModel
{
    public IEnumerable<PrescribingInformationClassificationViewDto>? PrescribingInformations { get; set; } 
}