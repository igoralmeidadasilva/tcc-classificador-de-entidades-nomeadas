namespace Classificador.Api.Application.Queries.GetPendingClassifications;

public sealed record GetPendingClassificationsQuery : IQuery<Result<GetPendingClassificationsQueryResponse>>
{
    public Guid IdUser { get; init; }
    public Guid IdPrescribingInformation { get; init; }
    public GetPendingClassificationsQuery(string idUser, string idPrescribingInformation)
    {
        IdUser = new Guid(idUser);
        IdPrescribingInformation = new Guid(idPrescribingInformation);
    }
}