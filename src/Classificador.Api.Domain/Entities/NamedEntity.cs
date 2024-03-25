using Classificador.Api.Domain.Interfaces;

namespace Classificador.Api.Domain.Entities;

public sealed record NamedEntity : BaseEntity<NamedEntity>, IEntity<NamedEntity>, IAggregateRoot
{
    public string Name { get; init; }
    public string? Description { get; private set; }

    public NamedEntity(string name, string? description = "") : base(Guid.NewGuid())
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
