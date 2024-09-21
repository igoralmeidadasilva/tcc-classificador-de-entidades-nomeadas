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
        await SeedingSpecialtyAsync(scope, cancellationToken);
    }

    private async Task SeedingSpecialtyAsync(IServiceScope scope, CancellationToken cancellationToken)
    {
        if(!_options.IsSpecialtySeedingActive)
        {
            _logger.LogInformation("The option to seed specialty data is disabled, skipping dataseeder.");
            return;
        }

        var context = scope.ServiceProvider.GetService<ClassifierContext>()
            ?? throw new Exception("An error occurred when trying to recover the Database.");
        
        if(context.Specialties.Any())
        {
            _logger.LogInformation("There are already specialty registered data, skipping dataseeder.");
            return;
        }

        await context!.Specialties.AddRangeAsync(_options.Specialties!, cancellationToken);
        await context!.SaveChangesAsync(cancellationToken);

        foreach(var specialty in _options.Specialties!)
        {
            _logger.LogInformation("Specialty '{SpecialtyName}' was entered with the Id {SpecialtyId}.",
                specialty.Name,
                specialty.Id);
        }
    }

    private async Task SeedingCategoryAsync(IServiceScope scope, CancellationToken cancellationToken)
    {
        if(!_options.IsCategorySeedingActive)
        {
            _logger.LogInformation("The option to seed category data is disabled, skipping dataseeder.");
            return;
        }

        var context = scope.ServiceProvider.GetService<ClassifierContext>() 
            ?? throw new Exception("An error occurred when trying to recover the Database.");
        
        if(context.Categories.Any())
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

        if(_options.Users is null)
        {
            _logger.LogInformation("There are no users to insert, skipping dataseeder.");
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

        var contextFactory = scope.ServiceProvider.GetService<IDbContextFactory<ClassifierContext>>() 
            ?? throw new Exception("An error occurred when trying to recover the Database.");

        using var context = contextFactory.CreateDbContext();

        await context.Database.MigrateAsync(cancellationToken);
        _logger.LogInformation("Migration executed successfully.");
    }
}
