namespace Classificador.Api.Application.Queries.GetDownloadSpacyModel;

public sealed record GetDownloadSpacyModelQuery : IQuery<Result>
{
    public Guid IdPrescribingInformation { get; init; }
    public string NamePrescribingInformation { get; init; }

    public GetDownloadSpacyModelQuery(string idPrescribingInformation, string namePrescribingInformation)
    {
        IdPrescribingInformation = new Guid(idPrescribingInformation);
        NamePrescribingInformation = namePrescribingInformation;
    }

}
