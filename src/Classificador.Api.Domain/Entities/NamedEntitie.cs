namespace Classificador.Api.Domain.Entities;

public sealed class NamedEntitie : EntityBase<NamedEntitie> 
{
    public int IdBiomedicalText { get; init ; }
    public NamedEntitieType IdNamedEntitieType { get; init ; }
    public string Word { get; init; } = string.Empty;
}
