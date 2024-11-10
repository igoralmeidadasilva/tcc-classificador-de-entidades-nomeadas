namespace Classificador.Api.Presentation.Models;

public sealed record CreatePrescribingInformationViewModel
{
    public IFormFile? File { get; set; }
    public string? Description { get; set; }
}
