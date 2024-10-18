namespace Classificador.Api.Domain.Entities;

public sealed class Category : Entity<Category>
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public ICollection<Classification>? Classifications { get; init; } = [];

    public Category() : base() {} //ORM

    private Category(Guid id, DateTime createdOnUtc, string name, string? description) : base(id, createdOnUtc)
    {
        Name = name;
        Description = description;
    }

    public static Category Create(string name, string? description)
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(name, nameof(Name));
        ArgumentValidator.ThrowIfNull(description!, nameof(Description));
        
        return new(Guid.NewGuid(), DateTime.UtcNow, name, description);
    }

    public override Category Update(Category entity)
    {
        Name = entity.Name;
        Description = entity.Description;
        
        return this;
    }
}
