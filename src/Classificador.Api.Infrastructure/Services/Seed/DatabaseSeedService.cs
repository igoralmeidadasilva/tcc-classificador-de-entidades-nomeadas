namespace Classificador.Api.Infrastructure.Services.Seed;

public sealed class DatabaseSeedService : IDatabaseSeedService
{
    private readonly DatabaseSeedOptions _options;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<DatabaseSeedService> _logger;

    public DatabaseSeedService(
        IOptions<DatabaseSeedOptions> options,
        IServiceScopeFactory scopeFactory,
        ILogger<DatabaseSeedService> logger)
    {
        _options = options.Value;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task ExecuteSeedAsync(CancellationToken cancellationToken = default)
    {
        if(!_options.IsSeedingActive)
        {
            _logger.LogInformation("The seed data option is disabled, skipping dataseeder.");
            return;
        }

        await SeedingUserAsync(cancellationToken);
    }

    private async Task SeedingUserAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetService<ClassifierContext>() 
            ?? throw new Exception("An error occurred when trying to recover the Database.");
        var passwordHashing = scope.ServiceProvider.GetService<IPasswordHashingService>()
            ?? throw new Exception("An error occurred when trying to recover the Hashing Password Service.");

        if(context.Users.Any())
        {
            _logger.LogInformation("There are already registered data, skipping dataseeder.");
            return;
        }

        await context!.Users.AddRangeAsync(UsersPasswordHashing(passwordHashing!, _options.Users!), cancellationToken);
        await context!.SaveChangesAsync(cancellationToken);

        foreach(var user in _options.Users!)
        {
            _logger.LogInformation("User {UserName} with the role of {UserRole} was entered with the Id {UserId}.",
                user.Name,
                user.Role,
                user.Id);
        }
    }

    private static List<User> UsersPasswordHashing(IPasswordHashingService service, ICollection<User> users)
    {
        var usersWithPasswordsIsHashing = users.Select(user => 
        {
            var passwordHashing = service.HashPassword(user.HashedPassword);
            return user.UpdateHashedPassword(passwordHashing);
        }).ToList();

        return usersWithPasswordsIsHashing;
    }

    public async Task ExecuteMigrationAsync(CancellationToken cancellationToken = default)
    {
        if(!_options.IsMigrationActive)
        {
            _logger.LogInformation("The option to automatically execute migrations is disabled, skipping migration.");
            return;
        }

        using IServiceScope scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetService<ClassifierContext>() 
            ?? throw new Exception("An error occurred when trying to recover the Database.");

        await context.Database.MigrateAsync(cancellationToken);
        _logger.LogInformation("Migration executed successfully.");
    }
}
