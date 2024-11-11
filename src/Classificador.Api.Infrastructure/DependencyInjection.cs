using Classificador.Api.Domain.Core.Enums;
using Classificador.Api.Domain.Core.Interfaces.Repositories.Persistence;
using Classificador.Api.Domain.Core.Interfaces.Repositories.ReadOnly;
using Classificador.Api.Domain.Core.Interfaces.Services;

namespace Classificador.Api.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services = services.AddDbContext(configuration);
        services = services.AddRepositories(configuration);
        services = services.AddCookieAuthentication(configuration);
        services = services.AddCustomAuthorization(configuration);
        services = services.AddServices(configuration);
        
        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("PostgreSQL");
        services.AddSingleton<SoftDeleteInterceptor>();

        services.AddDbContextFactory<MedTaggerContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(connectionString)
                .AddInterceptors(serviceProvider.GetRequiredService<SoftDeleteInterceptor>());;
        });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IClassificationPersistenceRepository, ClassificationPersistenceRepository>();
        services.AddScoped<INamedEntityPersistenceRepository, NamedEntityPersistenceRepository>();
        services.AddScoped<IUserPersistenceRepository, UserPersistenceRepository>();
        services.AddScoped<IPrescribingInformationPersistenceRepository, PrescribingInformationPersistenceRepository>();
        services.AddScoped<ICategoryPersistenceRepository, CategoryPersistenceRepository>();
        services.AddScoped<ISpecialtyPersistenceRepository, SpecialtyPersistenceRepository>();

        services.AddScoped<IClassificationReadOnlyRepository, ClassificationReadOnlyRepository>();
        services.AddScoped<INamedEntityReadOnlyRepository, NamedEntityReadOnlyRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserReadOnlyRepository>();
        services.AddScoped<IPrescribingInformationReadOnlyRepository, PrescribingInformationReadOnlyRepository>();
        services.AddScoped<ICategoryReadOnlyRepository, CategoryReadOnlyRepository>();
        services.AddScoped<ISpecialtyReadOnlyRepository, SpecialtyReadOnlyRepository>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPasswordHashingService, PasswordHashingService>();
        services.AddSingleton<IDatabaseSeedService, DatabaseSeedService>();
        services.AddSingleton<IEmailSenderService, EmailSenderService>();

        return services;
    }

    private static IServiceCollection AddCookieAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var expireTimeSpan = configuration.GetSection("CookieOptions:TokenExpirationInMinutes").Value;

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath =  new PathString("/auth/login");
            options.AccessDeniedPath = new PathString("/access-denied");
            options.ExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToInt32(expireTimeSpan));
            options.SlidingExpiration = true;
            options.Cookie.HttpOnly = true;
        });

        return services;
    }

    private static IServiceCollection AddCustomAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(nameof(UserRole.Padrao), policy => policy.RequireClaim(ClaimTypes.Role, UserRole.Padrao.ToString()))
            .AddPolicy(nameof(UserRole.Admin), policy => policy.RequireClaim(ClaimTypes.Role, UserRole.Admin.ToString()));

        return services;
    }

}
