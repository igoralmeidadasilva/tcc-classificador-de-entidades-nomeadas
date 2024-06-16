using Classificador.Api.Domain.Interfaces;

namespace Classificador.Api.Domain.Entities;

public sealed record NamedEntityPrescribingInformation
{
    public Guid IdNamedEntity { get; init; }
    public NamedEntity? NamedEntity { get; init; }
    public Guid IdPrescribingInformation { get; init; }
    public PrescribingInformation? PrescribingInformation { get; init; }
}
