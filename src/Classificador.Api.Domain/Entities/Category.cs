using Classificador.Api.Domain.Interfaces;

namespace Classificador.Api.Domain.Entities;

public sealed record Category : BaseEntity<Category>, IEntity<Category>
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public Category(string name, string description = "") : base(Guid.NewGuid())
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
