namespace Classificador.Api.Domain.Entities;

public sealed class NamedEntityPrescribingInformation
{
    public Guid IdNamedEntity { get; init; }
    public NamedEntity? NamedEntity { get; init; }
    public Guid IdPrescribingInformation { get; init; }
    public PrescribingInformation? PrescribingInformation { get; init; }
}
