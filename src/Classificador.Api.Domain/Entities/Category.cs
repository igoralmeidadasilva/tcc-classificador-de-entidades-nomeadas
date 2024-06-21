namespace Classificador.Api.Domain.Entities;

public sealed class Category : Entity<Category>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public ICollection<Classification>? Classifications { get; init; } = [];

    public Category(string name, string? description) : base()
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(name, nameof(name));
        ArgumentValidator.ThrowIfNull(description!, nameof(description));

        Name = name;
        Description = description;
    }

    public override Category Update(Category entity)
    {
        Name = entity.Name;
        Description = entity.Description;
        return this;
    }

}
