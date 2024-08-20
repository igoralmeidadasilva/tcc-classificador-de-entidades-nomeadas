namespace Classificador.Api.Application.Queries.GetAllClassificationByVotes;

public sealed class GetAllClassificationByVotesQueryValidator : AbstractValidator<GetAllClassificationByVotesQuery>
{
    public GetAllClassificationByVotesQueryValidator()
    {
        RuleFor(x => x.IdPrescribingInformation)
            .NotEmpty()
                .WithError(QueryErrors.GetAllClassificationByVotesFailures.PrescribingInformationIdIsRequired);
    }
}