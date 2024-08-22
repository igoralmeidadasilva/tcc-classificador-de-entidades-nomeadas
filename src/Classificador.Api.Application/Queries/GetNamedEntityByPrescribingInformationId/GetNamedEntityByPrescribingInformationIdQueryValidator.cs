namespace Classificador.Api.Application.Queries.GetNamedEntityByPrescribingInformationId;

public sealed class GetNamedEntityByPrescribingInformationIdQueryValidator : AbstractValidator<GetNamedEntityByPrescribingInformationIdQuery>
{
    public GetNamedEntityByPrescribingInformationIdQueryValidator()
    {
        RuleFor(x => x.IdPrescribingInformation)
            .NotEmpty()
                .WithError(QueryErrors.GetNamedEntityByPrescribingInformationIdFailures.PrescribingInformationIdIsRequired);

        RuleFor(x => x.IdUser)
            .NotEmpty()
                .WithError(QueryErrors.GetNamedEntityByPrescribingInformationIdFailures.UserIdIsRequired);
    }
}
