namespace Classificador.Api.Domain.Entities;

// TODO: Finalizar de pensar o relacionamento, essa classe é uma entidade ou um objeto de valor?
public sealed record Category 
{
    public string Name { get; init; }
    public string? Description { get; init; }

    public Category(string name, string description = "")
    {
        Name = name;
        Description = description;
    }

}
