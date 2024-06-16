namespace Classificador.Api.Domain.Entities;

public sealed record PrescribingInformation : Entity<PrescribingInformation>
{
    public string Name { get; init; }
    public string Text { get; init; }
    public string? Description { get; init; }
    public ICollection<NamedEntity>? NamedEntities { get; init; }  

    public PrescribingInformation(string name, string text, string? description = "") : base()
    {
        Name = name;
        Text = text;
        Description = description;
    }

    public override PrescribingInformation Update(PrescribingInformation entity)
    {
        throw new NotImplementedException();
    }

    public override void Validate()
    {
        throw new NotImplementedException();
    }

}
