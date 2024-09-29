namespace Classificador.Api.Presentation.Models;

public sealed record class PrecribingInformationClassificationsViewModel
{
    public string? NamePrescribingInformation { get; set; }
    public Guid IdPrescribingInformation { get; set; }
    public List<CountVoteForNamedEntity>? Classifications { get; set; }  = [];
}
