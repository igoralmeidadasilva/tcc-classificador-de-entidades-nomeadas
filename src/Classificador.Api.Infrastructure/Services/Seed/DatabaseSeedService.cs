namespace Classificador.Api.Infrastructure.Services.Seed;

public sealed class DatabaseSeedService(
    IOptions<DatabaseSeedOptions> options,
    IServiceScopeFactory scopeFactory,
    ILogger<DatabaseSeedService> logger) : IDatabaseSeedService
{
    private readonly DatabaseSeedOptions _options = options.Value;
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private readonly ILogger<DatabaseSeedService> _logger = logger;

    public async Task ExecuteSeedAsync(CancellationToken cancellationToken = default)
    {      
        using IServiceScope scope = _scopeFactory.CreateScope();

        await SeedingUserAsync(scope, cancellationToken);
        await SeedingCategoryAsync(scope, cancellationToken);
    }

    private async Task SeedingCategoryAsync(IServiceScope scope, CancellationToken cancellationToken)
    {
        if(!_options.IsCategorySeedingActive)
        {
            _logger.LogInformation("The option to seed user data is disabled, skipping dataseeder.");
            return;
        }

        var context = scope.ServiceProvider.GetService<ClassifierContext>() 
            ?? throw new Exception("An error occurred when trying to recover the Database.");
        
        if (context.Categories.Any())
        {
            _logger.LogInformation("There are already categories registered data, skipping dataseeder.");
            return;
        }

        await context!.Categories.AddRangeAsync(_options.Categories!, cancellationToken);
        await context!.SaveChangesAsync(cancellationToken);

        foreach(var category in _options.Categories!)
        {
            _logger.LogInformation("Category '{CategoryName}' was entered with the Id {CategoryId}.",
                category.Name,
                category.Id);
        }
    }

    private async Task SeedingUserAsync(IServiceScope scope , CancellationToken cancellationToken)
    {
        if(!_options.IsUserSeedingActive)
        {
            _logger.LogInformation("The option to seed user data is disabled, skipping dataseeder");
            return;
        }

        var context = scope.ServiceProvider.GetService<ClassifierContext>() 
            ?? throw new Exception("An error occurred when trying to recover the Database.");
        var passwordHashingService = scope.ServiceProvider.GetService<IPasswordHashingService>()
            ?? throw new Exception("An error occurred when trying to recover the Hashing Password Service.");

        if(context.Users.Any())
        {
            _logger.LogInformation("There are already users registered data, skipping dataseeder.");
            return;
        }

        var usersWithPasswordsIsHashing =  _options.Users!.Select(user => 
        {
            var passwordHashing = passwordHashingService.HashPassword(user.HashedPassword);
            return user.UpdateHashedPassword(passwordHashing);
        }).ToList();

        await context!.Users.AddRangeAsync(usersWithPasswordsIsHashing, cancellationToken);
        await context!.SaveChangesAsync(cancellationToken);

        foreach(var user in _options.Users!)
        {
            _logger.LogInformation("User {UserName} with the role of {UserRole} was entered with the Id {UserId}.",
                user.Name,
                user.Role,
                user.Id);
        }
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
