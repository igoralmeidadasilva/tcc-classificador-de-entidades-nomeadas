using Classificador.Api.Domain.Interfaces;

namespace Classificador.Api.Domain.Entities;

public sealed record NamedEntityHasPrescribingInformation : IValueObject
{
    public Guid IdNamedEntity { get; init; }
    public Guid IdPrescribingInformation { get; init; }
    public NamedEntity? NamedEntity { get; init; }
    public PrescribingInformation? PrescribingInformation { get; init; }
    
}
