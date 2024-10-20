namespace Classificador.Api.Application.Queries.DownloadSpacyModel;
public sealed record DownloadSpacyModelQuery : IQuery<Result<DownloadSpacyModelQueryResponse>>
{
    public Guid IdPrescribingInformation { get; init; }

    public DownloadSpacyModelQuery(Guid idPrescribingInformation)
    {
        IdPrescribingInformation = idPrescribingInformation;
    }
}
