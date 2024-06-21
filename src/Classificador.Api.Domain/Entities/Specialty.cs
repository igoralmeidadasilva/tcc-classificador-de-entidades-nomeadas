namespace Classificador.Api.Domain.Entities;

public sealed class Specialty : Entity<Specialty>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }    
    public ICollection<User>? Users { get; private set; } = [];

    public Specialty(string name, string? description)
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(name, nameof(name));
        ArgumentValidator.ThrowIfNull(description!, nameof(description));
        Name = name;
        Description = description;
    }

    public override Specialty Update(Specialty entity)
    {
        Name = entity.Name;
        Description = entity.Description;
        return this;
    }

}
