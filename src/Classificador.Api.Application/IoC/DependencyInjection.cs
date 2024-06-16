

namespace Classificador.Api.Application.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
