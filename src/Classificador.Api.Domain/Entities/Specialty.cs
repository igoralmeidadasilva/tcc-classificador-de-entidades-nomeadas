namespace Classificador.Api.Domain.Entities;

public sealed class Specialty : Entity<Specialty>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }    
    public ICollection<User>? Users { get; private set; } = [];
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
