namespace Classificador.Api.Domain.Entities;

public sealed record Specialty : Entity<Specialty>
{
    public string Name { get; set; }
    public string? Description { get; set; }    
    public ICollection<User>? Users { get; set; } = [];
    public Specialty(string name, string? description = "")
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
