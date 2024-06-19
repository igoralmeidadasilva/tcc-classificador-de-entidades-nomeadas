namespace Classificador.Api.Domain.Entities;

public sealed class Category : Entity<Category>
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public ICollection<Classification>? Classifications { get; init; } = [];

    public Category(string name, string description) : base()
    {
        Name = name;
        Description = description;

        Validate();
    }

    public Category(){} // ORM

    public override Category Update(Category entity)
    {
        Name = entity.Name;
        Description = entity.Description;
        return this;
    }

    public override void Validate()
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(Name!, nameof(Name));
        ArgumentValidator.ThrowIfNull(Description!, nameof(Description));
    }
}
