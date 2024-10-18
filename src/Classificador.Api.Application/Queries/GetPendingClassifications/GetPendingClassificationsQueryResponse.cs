using Classificador.Api.Application.Dtos;

namespace Classificador.Api.Application.Queries.GetPendingClassifications;

public sealed record GetPendingClassificationsQueryResponse : IQueryResponse
{
    public IEnumerable<ClassifyNamedEntityViewPendingClassificationDto> Response { get; init; } = [];
}
