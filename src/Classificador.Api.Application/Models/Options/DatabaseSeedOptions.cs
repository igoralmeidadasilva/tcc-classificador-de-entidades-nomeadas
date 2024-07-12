namespace Classificador.Api.Application.Models.Options;

public sealed record DatabaseSeedOptions
{
    public bool IsSeedingActive { get; init; }
    public bool IsMigrationActive { get; init; }
    public ICollection<User>? Users { get; init; }
}
