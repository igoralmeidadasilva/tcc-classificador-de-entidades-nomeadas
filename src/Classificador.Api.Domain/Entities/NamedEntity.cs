namespace Classificador.Api.Domain.Entities;

public sealed class NamedEntity : Entity<NamedEntity>, IAggregateRoot
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public ICollection<NamedEntityPrescribingInformation>? NamedEntityPrescribingsInformation { get; init; }  = [];
    public ICollection<Classification>? Classifications { get; init; }  = [];

    public NamedEntity(string name, string? description = "") : base()
    {
        Name = name;
        Description = description;
    }

    public override NamedEntity Update(NamedEntity entity)
    {
        throw new NotImplementedException();
    }

}
