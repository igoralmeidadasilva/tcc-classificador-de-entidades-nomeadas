namespace Classificador.Api.Domain.Entities;

// TODO: Finalizar de pensar o relacionamento, essa classe é uma entidade ou um objeto de valor?
public sealed record Specialty 
{
    public string Name { get; set; }
    public string? Description { get; set; }    
    public Specialty(string name, string? description = "")
    {
        Name = name;
        Description = description;
    }

}
