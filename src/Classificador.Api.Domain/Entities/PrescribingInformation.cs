namespace Classificador.Api.Domain.Entities;

public sealed class PrescribingInformation : Entity<PrescribingInformation>
{
    public string Name { get; private set; } = string.Empty;
    public string Text { get; private set; }  = string.Empty;
    public string? Description { get; private set; }
    public ICollection<NamedEntity>? NamedEntities{ get; set; }

    public PrescribingInformation(string name, string text, string? description) : base()
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(name, nameof(name));
        ArgumentValidator.ThrowIfNullOrWhitespace(text, nameof(text));
        ArgumentValidator.ThrowIfNull(description!, nameof(description));

        Name = name;
        Text = text;
        Description = description;
    }

    public PrescribingInformation() {} //ORM

    public override PrescribingInformation Update(PrescribingInformation entity)
    {
        Name = entity.Name;
        Text = entity.Text;
        Description = entity.Description;
        return this;
    }

}
