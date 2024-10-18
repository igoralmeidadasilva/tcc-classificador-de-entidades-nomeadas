using Classificador.Api.Application.Behaviors;
using Classificador.Api.Application.Commands.CreateCategory;
using Classificador.Api.Application.Commands.CreateClassification;
using Classificador.Api.Application.Commands.CreatePrescribingInformationTxt;
using Classificador.Api.Application.Commands.CreateSpecialty;
using Classificador.Api.Application.Commands.CreateUser;
using Classificador.Api.Application.Commands.LoginUser;
using Classificador.Api.Application.Commands.SendEmailToContact;
using Classificador.Api.Application.Commands.UpdateUserRoleToAdmin;
using Classificador.Api.Application.Commands.UpdateUserRoleToStandard;
using Classificador.Api.Application.Models.Options;
using Classificador.Api.Application.Queries.GetNamedEntityByPrescribingInformationId;
using Classificador.Api.Application.Queries.GetPendingClassifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Classificador.Api.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services = services.AddMediatr(configuration);
        services = services.AddValidator(configuration);
        services = services.AddAutoMapper(configuration);
        services = services.AddOptions(configuration);

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
        services.AddScoped<IValidator<CreateCategoryCommand>, CreateCategoryCommandValidator>();
        services.AddScoped<IValidator<CreateSpecialtyCommand>, CreateSpecialtyCommandValidator>();
        services.AddScoped<IValidator<SendEmailToContactCommand>, SendEmailToContactCommandValidator>();
        services.AddScoped<IValidator<CreateClassificationCommand>, CreateClassificationCommandValidator>();
        services.AddScoped<IValidator<GetPendingClassificationsQuery>, GetPendingClassificationsQueryValidator>();
        services.AddScoped<IValidator<GetNamedEntityByPrescribingInformationIdQuery>, GetNamedEntityByPrescribingInformationIdQueryValidator>();
        
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

        services.Configure<EmailOptions>(options => configuration.GetSection(nameof(EmailOptions))
            .Bind(options));

        return services;
    }
}
