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
        services.AddSingleton<IJwtSecurityTokenService, JwtSecurityTokenService>();
        services.AddSingleton<IEmailSenderService, EmailSenderService>();

        return services;
    }

    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options => 
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(opt =>
        {
            opt.RequireHttpsMetadata = false;
            opt.SaveToken = true;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JwtOptions:TokenSecurityKey").Value!)),
            };
        });

        return services;
    }

    private static IServiceCollection AddCookieAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var expireTimeSpan = configuration.GetSection("CookieOptions:TokenExpirationInMinutes").Value;

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath =  new PathString("/Home/Login");
            options.AccessDeniedPath = new PathString("/Home/AccessDenied");
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
