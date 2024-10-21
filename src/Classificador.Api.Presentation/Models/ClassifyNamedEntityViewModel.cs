namespace Classificador.Api.Presentation.Models;

public sealed record ClassifyNamedEntityViewModel
{
    public Guid IdPrescribingInformation { get; set; }
    public string? NamePrescribingInformation { get; set; }
    public int NamedEntityIndex { get; set; }
    public List<ClassifyNamedEntityViewCategoryDto>? Categories { get; set; } 
    public CreateClassificationForm? CreateClassificationForm { get; set;}
    public PatchClassificationToCompletedForm? PatchClassificationForm { get; set; }
    public DeletePendingClassificationForm? DeletePendingClassificationForm { get; set; }
}

public sealed record CreateClassificationForm
{
    public Guid IdUser { get; set; } 
    public Guid IdNamedEntity { get; set; }
    public Guid IdCategory { get; set; }
    public string? Comment { get; set; }

    public static implicit operator CreateClassificationCommand(CreateClassificationForm viewModel)
    {
        return new()
        {
            IdUser = viewModel.IdUser,
            IdNamedEntity = viewModel.IdNamedEntity,
            IdCategory = viewModel.IdCategory,
            Comment = viewModel.Comment ?? string.Empty
        };
    }
}

public sealed record PatchClassificationToCompletedForm
{
    public Guid IdUser { get; set; } 
    public Guid IdPrescribingInformation { get; set; } 

    public static implicit operator UpdateClassificationToCompletedCommand(PatchClassificationToCompletedForm viewModel)
    {
        return new()
        {
            IdUser = viewModel.IdUser,
            IdPrescribingInformation = viewModel.IdPrescribingInformation
        };
    }
}

public sealed record DeletePendingClassificationForm
{
    public Guid IdClassification { get; set; }

    public static implicit operator DeletePendingClassificationCommand(DeletePendingClassificationForm viewModel)
    {
        return new()
        {
            IdClassification = viewModel.IdClassification
        };
    }
}