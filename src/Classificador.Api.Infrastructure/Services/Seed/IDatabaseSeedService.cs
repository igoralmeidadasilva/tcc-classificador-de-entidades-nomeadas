namespace Classificador.Api.Infrastructure.Services.Seed;

public interface IDatabaseSeedService
{
    Task ExecuteSeedAsync(CancellationToken cancellationToken = default);
    Task ExecuteMigrationAsync(CancellationToken cancellationToken = default);
}