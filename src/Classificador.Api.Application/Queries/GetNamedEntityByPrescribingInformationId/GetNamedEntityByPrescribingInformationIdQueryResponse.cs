using Classificador.Api.Application.Dtos;

namespace Classificador.Api.Application.Queries.GetNamedEntityByPrescribingInformationId;

public sealed record GetNamedEntityByPrescribingInformationIdQueryResponse : IQueryResponse
{
    public IEnumerable<ClassifyNamedEntityViewNamedEntityDto> Response { get; init; } = [];
}
