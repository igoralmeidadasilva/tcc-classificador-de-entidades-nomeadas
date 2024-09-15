namespace Classificador.Api.Presentation.Models;

public sealed record ClassifyNamedEntityViewModel
{
    public Guid IdPrescribingInformation { get; set; }
    public string? NamePrescribingInformation { get; set; }
    public int NamedEntityIndex { get; set; }
    public List<ClassifyNamedEntityViewCategoryDto>? Categories { get; set; } 
    public CreateClassificationViewModel? CreateClassificationForm { get; set;}
    public PatchClassificationToCompletedViewModel? PatchClassificationForm { get; set; }
    public DeletePendingClassificationViewModel? DeletePendingClassificationForm { get; set; }
}

public sealed record CreateClassificationViewModel
{
    public Guid IdUser { get; set; } 
    public Guid IdNamedEntity { get; set; }
    public Guid IdCategory { get; set; }
    public string? Comment { get; set; }

    public static implicit operator CreateClassificationCommand(CreateClassificationViewModel viewModel)
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

public sealed record PatchClassificationToCompletedViewModel
{
    public Guid IdUser { get; set; } 
    public Guid IdPrescribingInformation { get; set; } 

    public static implicit operator UpdateClassificationToCompletedCommand(PatchClassificationToCompletedViewModel viewModel)
    {
        return new()
        {
            IdUser = viewModel.IdUser,
            IdPrescribingInformation = viewModel.IdPrescribingInformation
        };
    }
}

public sealed record DeletePendingClassificationViewModel
{
    public Guid IdClassification { get; set; }

    public static implicit operator DeletePendingClassificationCommand(DeletePendingClassificationViewModel viewModel)
    {
        return new()
        {
            IdClassification = viewModel.IdClassification
        };
    }
}