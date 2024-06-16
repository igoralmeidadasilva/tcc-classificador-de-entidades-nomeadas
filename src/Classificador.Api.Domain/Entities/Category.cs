namespace Classificador.Api.Domain.Entities;

public sealed record Category : Entity<Category>
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public ICollection<Classification>? Classifications { get; init; } = [];

    public Category(string name, string description = "") : base()
    {
        Name = name;
        Description = description;
    }

    public override Category Update(Category entity)
    {
        throw new NotImplementedException();
    }

    public override void Validate()
    {
        throw new NotImplementedException();
    }
}
