using Classificador.Api.Domain.Core.Interfaces;

namespace Classificador.Api.Domain.Entities;

public sealed class Specialty : Entity<Specialty>, ISoftDeletableEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }    
    public ICollection<User>? Users { get; private set; } = [];
    public bool IsDeleted {get; private set; }
    public DateTime DeletedOnUtc { get; private set; }

    public Specialty() : base() {} // ORM

    private Specialty(Guid id, DateTime createdOnUtc, string name, string? description) : base(id, createdOnUtc)
    {
        Name = name;
        Description = description;
    }

    public static Specialty Create(string name, string? description)
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(name, nameof(Name));
        ArgumentValidator.ThrowIfNull(description!, nameof(Description));

        return new(Guid.NewGuid(), DateTime.UtcNow, name, description);
    }

    public override Specialty Update(Specialty entity)
    {
        Name = entity.Name;
        Description = entity.Description;

        return this;
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedOnUtc = DateTime.UtcNow;
    }

    public void Restore()
    {
        IsDeleted = false;
    }
}
