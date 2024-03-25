using Classificador.Api.Domain.Interfaces;

namespace Classificador.Api.Domain.Entities;

public sealed record BiomedicalText : BaseEntity<BiomedicalText>, IEntity<BiomedicalText>
{
    public string Name { get; private set; }
    public string Text { get; private set; }
    public string? Description { get; private set; }

    public BiomedicalText(string name, string text, string description = "") : base(Guid.NewGuid())
    {
        Name = name;
        Text = text;
        Description = description;
    }

    public override BiomedicalText Update(BiomedicalText entity)
    {
        throw new NotImplementedException();
    }

    public override void Validate()
    {
        throw new NotImplementedException();
    }
}
