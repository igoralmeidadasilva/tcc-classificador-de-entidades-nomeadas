namespace Classificador.Api.Domain.Entities;

public sealed class NamedEntity : Entity<NamedEntity>, IAggregateRoot
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public Guid? IdPrescribingInformation { get; init; }
    public PrescribingInformation? PrescribingInformation { get; init; }
    public ICollection<Classification>? Classifications { get; init; }  = [];

    public NamedEntity(string name, string? description) : base()
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(name, nameof(name));
        ArgumentValidator.ThrowIfNull(description!, nameof(description));

        Name = name;
        Description = description;
    }

    public override NamedEntity Update(NamedEntity entity)
    {
        Name = entity.Name;
        Description = entity.Description;
        return this;
    }

}
