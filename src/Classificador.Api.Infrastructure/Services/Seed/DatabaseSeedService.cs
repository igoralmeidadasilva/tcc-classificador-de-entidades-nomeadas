using Classificador.Api.Domain.Core.Interfaces.Services;

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

        var context = scope.ServiceProvider.GetService<MedTaggerContext>()
            ?? throw new Exception("An error occurred when trying to recover the Database.");
        
        if(context.Specialties.Any())
        {
            _logger.LogInformation("There are already specialty registered data, skipping dataseeder.");
            return;
        }

        var specialtiesToInsert =  _options.Specialties!.Select(specialty => 
        {
            Specialty newSpecialty = Specialty.Create(specialty.Name, specialty.Description);

            return newSpecialty;
        }).ToList();

        await context!.Specialties.AddRangeAsync(specialtiesToInsert, cancellationToken);
        await context!.SaveChangesAsync(cancellationToken);

        foreach(var specialty in specialtiesToInsert)
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

        var context = scope.ServiceProvider.GetService<MedTaggerContext>() 
            ?? throw new Exception("An error occurred when trying to recover the Database.");
        
        if(context.Categories.Any())
        {
            _logger.LogInformation("There are already categories registered data, skipping dataseeder.");
            return;
        }

        var categoriesToInsert =  _options.Categories!.Select(category => 
        {
            Category newCategory = Category.Create(category.Name, category.Description);

            return newCategory;
        }).ToList();

        await context!.Categories.AddRangeAsync(categoriesToInsert, cancellationToken);
        await context!.SaveChangesAsync(cancellationToken);

        foreach(var category in categoriesToInsert)
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

        var context = scope.ServiceProvider.GetService<MedTaggerContext>() 
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

        var usersToInsert =  _options.Users!.Select(user => 
        {
            User newUser = User.Create(user.Email, user.HashedPassword, user.Name, user.IdSpecialty, user.Contact);
            newUser = newUser.UpdateRole(user.Role);
            var passwordHashing = passwordHashingService.HashPassword(newUser.HashedPassword);
            return newUser.UpdateHashedPassword(passwordHashing);
        }).ToList();

        await context!.Users.AddRangeAsync(usersToInsert, cancellationToken);
        await context!.SaveChangesAsync(cancellationToken);

        foreach(var user in usersToInsert)
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

        var contextFactory = scope.ServiceProvider.GetService<IDbContextFactory<MedTaggerContext>>() 
            ?? throw new Exception("An error occurred when trying to recover the Database.");

        using var context = contextFactory.CreateDbContext();

        await context.Database.MigrateAsync(cancellationToken);
        _logger.LogInformation("Migration executed successfully.");
    }
}
