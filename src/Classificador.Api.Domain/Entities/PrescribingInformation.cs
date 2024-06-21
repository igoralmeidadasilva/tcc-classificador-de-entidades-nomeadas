namespace Classificador.Api.Domain.Entities;

public sealed class PrescribingInformation : Entity<PrescribingInformation>
{
    public string Name { get; private set; }
    public string Text { get; private set; }
    public string? Description { get; private set; }
    public ICollection<NamedEntityPrescribingInformation>? NamedEntityPrescribingsInformation { get; init; } = []; 

    public PrescribingInformation(string name, string text, string? description) : base()
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(name, nameof(name));
        ArgumentValidator.ThrowIfNull(text, nameof(text));
        ArgumentValidator.ThrowIfNull(description!, nameof(description));

        Name = name;
        Text = text;
        Description = description;
    }

    public override PrescribingInformation Update(PrescribingInformation entity)
    {
        Name = entity.Name;
        Text = entity.Text;
        Description = entity.Description;
        return this;
    }

}
