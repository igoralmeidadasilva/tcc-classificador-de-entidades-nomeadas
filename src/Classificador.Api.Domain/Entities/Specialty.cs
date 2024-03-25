using Classificador.Api.Domain.Interfaces;

namespace Classificador.Api.Domain.Entities;

public sealed record Specialty : BaseEntity<Specialty>, IEntity<Specialty>
{
    public string Name { get; set; }
    public string? Description { get; set; }    
    public ICollection<UserSpecialty>? Specializations { get; set; }
    public Specialty(Guid id, DateTime createdAt, string name, string? description = "") : base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
    }

    public override Specialty Update(Specialty entity)
    {
        throw new NotImplementedException();
    }

    public override void Validate()
    {
        throw new NotImplementedException();
    }
}
