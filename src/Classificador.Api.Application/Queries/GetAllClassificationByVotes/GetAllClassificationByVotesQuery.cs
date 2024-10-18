namespace Classificador.Api.Application.Queries.GetAllClassificationByVotes;

public sealed record GetAllClassificationByVotesQuery : IQuery<Result<GetAllClassificationByVotesQueryResponse>>
{
    public Guid IdPrescribingInformation { get; init; }

    public GetAllClassificationByVotesQuery(string idPrescribingInformation)
    {
        IdPrescribingInformation = new Guid(idPrescribingInformation);
    }
}