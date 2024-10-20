namespace Classificador.Api.Presentation.Models;

public sealed class ClassifiedPrescribingInformationDetailsViewModel
{
    public string? NamePrescribingInformation { get; set; }
    public Guid IdPrescribingInformation { get; set; }
    public IEnumerable<CountVoteForNamedEntity>? Classifications { get; set; }
}
