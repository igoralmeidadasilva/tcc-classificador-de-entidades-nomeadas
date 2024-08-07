namespace Classificador.Api.Domain.Entities;

public sealed class NamedEntity : Entity<NamedEntity>, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public WordPosition WordPosition { get; private set; }  = new WordPosition();
    public Guid? IdPrescribingInformation { get; init; }
    public PrescribingInformation? PrescribingInformation { get; init; }
    public ICollection<Classification>? Classifications { get; init; }  = [];

    public NamedEntity() {} // ORM

    public NamedEntity(string name, string? description, WordPosition wordPosition) : base()
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(name, nameof(name));
        ArgumentValidator.ThrowIfNull(description!, nameof(description));

        Name = name;
        Description = description;
        WordPosition = wordPosition;
    }

    public override NamedEntity Update(NamedEntity entity)
    {
        Name = entity.Name;
        Description = entity.Description;
        return this;
    }

}
