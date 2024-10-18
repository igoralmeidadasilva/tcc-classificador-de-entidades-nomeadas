using Classificador.Api.Application.Dtos;

namespace Classificador.Api.Application.Queries.GetGetPrescribingInformation;

public sealed record GetPrescribingInformationQueryResponse : IQueryResponse
{
    public IEnumerable<PrescribingInformationClassificationViewDto> Response { get; init; } = [];
}
