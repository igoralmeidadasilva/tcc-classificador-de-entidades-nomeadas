namespace Classificador.Api.Infrastructure.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services = AddDbContext(services, configuration);
        services = AddRepositories(services, configuration);
        services = AddAuthentication(services, configuration);
        services = AddCustomAuthorization(services, configuration);
        services = AddServices(services, configuration);
        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgreSQL");

        services.AddSingleton<SoftDeleteInterceptor>();

        services.AddDbContext<ClassifierContext>
        (   
            (sp, opt) => opt.UseNpgsql(connectionString)
                .AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>())
        );

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IClassificationPersistenceRepository, ClassificationPersistenceRepository>();
        services.AddScoped<INamedEntityPersistenceRepository, NamedEntityPersistenceRepository>();
        services.AddScoped<IUserPersistenceRepository, UserPersistenceRepository>();
        
        services.AddScoped<IClassificationReadOnlyRepository, ClassificationReadOnlyRepository>();
        services.AddScoped<INamedEntityReadOnlyRepository, NamedEntityReadOnlyRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserReadOnlyRepository>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPasswordHashingService, PasswordHashingService>();
        services.AddSingleton<IDatabaseSeedService, DatabaseSeedService>();
        services.AddSingleton<ITokenService, TokenService>();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opt => 
        {
            opt.RequireHttpsMetadata = true;
            opt.SaveToken = true;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JwtOptions:TokenSecurityKey").Value!)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
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
