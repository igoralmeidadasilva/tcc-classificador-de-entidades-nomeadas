namespace Classificador.Api.Application.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services = AddMediatr(services, configuration);
        services = AddValidator(services, configuration);
        services = AddAutoMapper(services, configuration);
        services = AddOptions(services, configuration);
        return services;
    }

    private static IServiceCollection AddMediatr(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(opt => 
        {
            opt.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        });

        return services;
    }

    private static IServiceCollection AddValidator(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
        services.AddScoped<IValidator<UpdateUserRoleToAdminCommand>, UpdateUserRoleToAdminCommandValidator>();
        services.AddScoped<IValidator<UpdateUserRoleToStandardCommand>, UpdateUserRoleToStandardCommandValidator>();
        services.AddScoped<IValidator<LoginUserCommand>, LoginUserCommandValidator>();
        services.AddScoped<IValidator<CreatePrescribingInformationTxtCommand>, CreatePrescribingInformationTxtCommandValidator>();
        services.AddScoped<IValidator<CreateCategoryCommand>,CreateCategoryCommandValidator>();

        return services;
    }

    private static IServiceCollection AddAutoMapper(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSeedOptions>(options => configuration.GetSection(nameof(DatabaseSeedOptions))
            .Bind(options, c => c.BindNonPublicProperties = true));

        services.Configure<JwtOptions>(options => configuration.GetSection(nameof(JwtOptions))
            .Bind(options));

        return services;
    }
}
