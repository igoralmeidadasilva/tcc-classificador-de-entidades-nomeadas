namespace Classificador.Api.Domain.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
