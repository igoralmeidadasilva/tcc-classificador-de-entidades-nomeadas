namespace Classificador.Api.Application.Models.Options;

public sealed record DatabaseSeedOptions
{
    public bool IsUserSeedingActive { get; init; }
    public bool IsCategorySeedingActive { get; init; }
    public bool IsMigrationActive { get; init; }
    public bool IsSpecialtySeedingActive { get; init; }
    public ICollection<User>? Users { get; init; }
    public ICollection<Category>? Categories { get; init; }
    public ICollection<Specialty>? Specialties { get; init; }
}
