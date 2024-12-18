using Classificador.Api.Domain.Core.Interfaces;
using Classificador.Api.Domain.ValueObjects.NamedEntity;

namespace Classificador.Api.Domain.Entities;

public sealed class NamedEntity : Entity<NamedEntity>, ISoftDeletableEntity, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public WordPosition WordPosition { get; private set; }  = new WordPosition();
    public Guid? IdPrescribingInformation { get; init; }
    public PrescribingInformation? PrescribingInformation { get; init; }
    public ICollection<Classification>? Classifications { get; init; }  = [];
    public bool IsDeleted {get; private set; }
    public DateTime? DeletedOnUtc { get; private set; }

    public NamedEntity() : base() {} // ORM

    private NamedEntity(Guid id, DateTime createdOnUtc, string name, string? description, WordPosition wordPosition) : base(id, createdOnUtc)
    {
        Name = name;
        Description = description;
        WordPosition = wordPosition;
    }

    public static NamedEntity Create(string name, string? description, WordPosition wordPosition)
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(name, nameof(Name));
        ArgumentValidator.ThrowIfNull(description!, nameof(Description));
        ArgumentValidator.ThrowIfNull(wordPosition!, nameof(WordPosition));

        return new(Guid.NewGuid(), DateTime.UtcNow, name, description, wordPosition);
    }

    public override NamedEntity Update(NamedEntity entity)
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
