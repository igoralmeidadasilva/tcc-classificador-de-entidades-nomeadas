using Classificador.Api.Application.Dtos;

namespace Classificador.Api.Application.Queries.GetPrescribingInformationById;

public sealed record GetPrescribingInformationByIdQueryResponse : IQueryResponse
{
    public IEnumerable<ChoosePrescribingInformationViewDto> Response { get; init; } = [];
}
