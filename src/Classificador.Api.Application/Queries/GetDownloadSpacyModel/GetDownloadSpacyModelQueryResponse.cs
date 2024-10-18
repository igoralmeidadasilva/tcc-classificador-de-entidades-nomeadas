using Classificador.Api.Domain.Models;

namespace Classificador.Api.Application.Queries.GetDownloadSpacyModel;

public sealed record GetDownloadSpacyModelQueryResponse : IQueryResponse
{
    public string? Text { get; init; }
    public IEnumerable<SpacyNerModel>? Entities { get; init; }
}
