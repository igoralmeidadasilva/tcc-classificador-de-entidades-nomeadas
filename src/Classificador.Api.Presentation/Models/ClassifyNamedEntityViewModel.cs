namespace Classificador.Api.Presentation.Models;

public sealed record ClassifyNamedEntityViewModel
{
    public Guid PrescribingInformationIdId { get; set; }
    public List<ClassifyNamedEntityViewCategoryDto>? Categories { get; set; } 
}