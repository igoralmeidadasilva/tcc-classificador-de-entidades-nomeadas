using Classificador.Api.Domain.Interfaces;

namespace Classificador.Api.Domain.Entities;

public sealed record NamedEntity : Entity<NamedEntity>, IAggregateRoot
{
    public string Name { get; init; }
    public string? Description { get; init; }
    // TODO: Revisar esse relacionamento, ao meu ver, aqui nunca teremos um relacionamento 'N' para PrescribingInformation
    public ICollection<PrescribingInformation>? PrescribingInformation { get; init; }

    public NamedEntity(string name, string? description = "") : base()
    {
        Name = name;
        Description = description;
    }

    public override NamedEntity Update(NamedEntity entity)
    {
        throw new NotImplementedException();
    }

    public override void Validate()
    {
        throw new NotImplementedException();
    }

}
