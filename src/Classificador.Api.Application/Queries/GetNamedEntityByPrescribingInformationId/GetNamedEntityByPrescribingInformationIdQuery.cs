using Classificador.Api.Application.Dtos;

namespace Classificador.Api.Application.Queries.GetNamedEntityByPrescribingInformationId;

public sealed record GetNamedEntityByPrescribingInformationIdQuery : IQuery<Result<IEnumerable<ClassifyNamedEntityViewNamedEntityDto>>>
{
    public Guid IdPrescribingInformation { get; init; }
    public Guid IdUser { get; init; }

    public GetNamedEntityByPrescribingInformationIdQuery(Guid idPrescribingInformation, Guid idUser)
    {
        IdPrescribingInformation = idPrescribingInformation;
        IdUser = idUser;
    }
}