using Classificador.Api.Application.Core.Errors;

namespace Classificador.Api.Application.Queries.GetPendingClassifications;

public sealed class GetPendingClassificationsQueryValidator : AbstractValidator<GetPendingClassificationsQuery>
{
    public GetPendingClassificationsQueryValidator()
    {
        RuleFor(x => x.IdPrescribingInformation)
            .NotEmpty()
                .WithError(QueryErrors.GetPendingClassificationsFailures.PrescribingInformationIdIsRequired);

        RuleFor(x => x.IdUser)
            .NotEmpty()
                .WithError(QueryErrors.GetPendingClassificationsFailures.UserIdIsRequired);
    }
}