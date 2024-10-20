using Classificador.Api.Domain.Models;
using Newtonsoft.Json;

namespace Classificador.Api.Application.Queries.DownloadSpacyModel;

public sealed record DownloadSpacyModelQueryResponse : IQueryResponse
{
    [JsonIgnore]
    public string? Name { get; set;}
    public string? Text { get; init; }
    public IEnumerable<SpacyNerModel>? Entities { get; init; }
}
