namespace Classificador.Api.Application.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services = AddMediatr(services, configuration);
        services = AddValidator(services, configuration);
        services = AddAutoMapper(services, configuration);
        return services;
    }

    private static IServiceCollection AddMediatr(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(opt => 
        {
            opt.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        return services;
    }

    private static IServiceCollection AddValidator(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();

        return services;
    }

    private static IServiceCollection AddAutoMapper(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
}
