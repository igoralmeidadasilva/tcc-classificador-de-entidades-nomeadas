namespace Classificador.Api.Application.Queries.GetPendingClassifications;

public sealed class GetPendingClassificationsQueryValidator : AbstractValidator<GetPendingClassificationsQuery>
{
    public GetPendingClassificationsQueryValidator()
    {
        RuleFor(x => x.IdPrescribingInformation)
            .NotEmpty()
                .WithError(CommandErrors.UpdateUserRoleFailures.UserIdIsRequired);

        RuleFor(x => x.IdUser)
            .NotEmpty()
                .WithError(CommandErrors.UpdateUserRoleFailures.UserIdIsRequired);
    }
}