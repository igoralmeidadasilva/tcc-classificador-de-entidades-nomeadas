using Classificador.Api.Infrastructure.Interceptors;
using Classificador.Api.Infrastructure.Repositories.Persistence;
using Classificador.Api.Infrastructure.Repositories.ReadOnly;

namespace Classificador.Api.Infrastructure.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services = AddDbContext(services, configuration);
        services = AddRepositories(services);
        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgreSQL");
        services.AddDbContext<ClassifierContext>
        (   
            (sp, opt) => opt.UseNpgsql(connectionString)
                .AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>())
        );

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IClassificationPersistenceRepository, ClassificationPersistenceRepository>();
        services.AddScoped<INamedEntityPersistenceRepository, NamedEntityPersistenceRepository>();
        services.AddScoped<IUserPersistenceRepository, UserPersistenceRepository>();
        
        services.AddScoped<IClassificationReadOnlyRepository, ClassificationReadOnlyRepository>();
        services.AddScoped<INamedEntityReadOnlyRepository, NamedEntityReadOnlyRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserReadOnlyRepository>();

        return services;
    }
}
