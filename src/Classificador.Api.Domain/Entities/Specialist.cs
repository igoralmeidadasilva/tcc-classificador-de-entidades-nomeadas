namespace Classificador.Api.Domain.Entities;

public sealed class Specialist : EntityBase<Specialist>
{
    public string Name { get; private set; } = string.Empty;
    public string Specialties { get; private set; } = string.Empty; 
}
