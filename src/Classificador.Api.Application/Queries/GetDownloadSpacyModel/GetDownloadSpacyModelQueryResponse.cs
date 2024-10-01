namespace Classificador.Api.Application.Queries.GetDownloadSpacyModel;

public sealed class GetDownloadSpacyModelQueryResponse
{
    public string? Text { get; set; }
    public IEnumerable<SpacyNerModel>? Entities { get; set; }
}
