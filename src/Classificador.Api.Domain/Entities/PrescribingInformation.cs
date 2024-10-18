namespace Classificador.Api.Domain.Entities;

public sealed class PrescribingInformation : Entity<PrescribingInformation>
{
    public string Name { get; private set; } = string.Empty;
    public string Text { get; private set; }  = string.Empty;
    public string? Description { get; private set; }
    public ICollection<NamedEntity>? NamedEntities{ get; set; }

    public PrescribingInformation() : base() {} //ORM

    private PrescribingInformation(Guid id, DateTime createdOnUtc, string name, string text, string? description) : base(id, createdOnUtc)
    {
        Name = name;
        Text = text;
        Description = description;
    }

    public static PrescribingInformation Create(string name, string text, string? description)
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(name, nameof(Name));
        ArgumentValidator.ThrowIfNullOrWhitespace(text, nameof(Text));
        ArgumentValidator.ThrowIfNull(description!, nameof(Description));

        return new(Guid.NewGuid(), DateTime.UtcNow, name, text, description);
    }

    public override PrescribingInformation Update(PrescribingInformation entity)
    {
        Name = entity.Name;
        Text = entity.Text;
        Description = entity.Description;

        return this;
    }
}
