namespace Classificador.Api.Presentation.Models;

public sealed record ClassifyNamedEntityViewModel
{
    public Guid IdPrescribingInformation { get; set; }
    public List<ClassifyNamedEntityViewCategoryDto>? Categories { get; set; } 
    public int NamedEntityIndex { get; set; }
    public Guid IdUser { get; set; }
    public Guid IdNamedEntity { get; set; }
    public Guid IdCategory { get; set; }
    public string? NamePrescribingInformation { get; set; }
    public string? Comment { get; set; } = string.Empty;

    public static implicit operator CreateClassificationCommand(ClassifyNamedEntityViewModel viewModel) =>
        new(viewModel.IdUser, viewModel.IdNamedEntity, viewModel.IdCategory, viewModel.Comment);
}