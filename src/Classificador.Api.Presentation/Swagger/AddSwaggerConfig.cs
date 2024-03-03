using Microsoft.OpenApi.Models;

namespace Classificador.Api.Presentation.IoC.Swagger;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Classificador de Entidades Nomeadas API",
                Description = "Trabalho de conclusão de curso em bacharelado de sistemas de informação IFES cachoeiro de itapemirim"
                // TermsOfService = new Uri("https://example.com/terms"),
                // Contact = new OpenApiContact
                // {
                //     Name = "Example Contact",
                //     Url = new Uri("https://example.com/contact")
                // },
                // License = new OpenApiLicense
                // {
                //     Name = "Example License",
                //     Url = new Uri("https://example.com/license")
                // }
            });
        });

        return services;
    }

}
