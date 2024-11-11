namespace Classificador.Api.Presentation.Models;

public sealed record ClassifyNamedEntityViewModel
{
    public Guid IdPrescribingInformation { get; set; }
    public int NamedEntityIndex { get; set; }
    public IReadOnlyList<ClassifyNamedEntityViewNamedEntityDto> NamedEntities { get; set; } = [];
    public IReadOnlyList<ClassifyNamedEntityViewCategoryDto>? Categories { get; set; } = [];
    public IReadOnlyList<ClassifyNamedEntityViewPendingClassificationDto>? PendingClassifications { get; set; } = [];
    public CreateClassificationForm? CreateClassificationForm { get; set; }
    public PatchClassificationToCompletedForm? PatchClassificationForm { get; set; }
    public DeletePendingClassificationForm? DeletePendingClassificationForm { get; set; }
    public bool HasNext() => NamedEntityIndex < NamedEntities.Count - 1;
    public bool HasPrev() => NamedEntityIndex > 0;  
    public ClassifyNamedEntityViewNamedEntityDto ActualEntity()
    {
        if(NamedEntities.Any())
        {
            return NamedEntities[NamedEntityIndex];
        }
        return new ClassifyNamedEntityViewNamedEntityDto();
    }

}

public sealed record CreateClassificationForm
{
    public Guid IdUser { get; set; } 
    public Guid IdNamedEntity { get; set; }
    public Guid IdCategory { get; set; }
    public string? Comment { get; set; }
}

public sealed record DeletePendingClassificationForm
{
    public Guid IdClassification { get; set; }
}

public sealed record PatchClassificationToCompletedForm
{
    public Guid IdUser { get; set; } 
    public Guid IdPrescribingInformation { get; set; } 
}
