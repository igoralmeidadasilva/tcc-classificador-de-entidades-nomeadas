using Classificador.Api.Application.Dtos;

namespace Classificador.Api.Application.Queries.GetNamedEntityByPrescribingInformationId;

public sealed record GetNamedEntityByPrescribingInformationIdQuery : IQuery<Result<GetNamedEntityByPrescribingInformationIdQueryResponse>>
{
    public Guid IdPrescribingInformation { get; set; }
    public Guid IdUser { get; set; }

    public GetNamedEntityByPrescribingInformationIdQuery(string idPrescribingInformation, string idUser)
    {
        IdPrescribingInformation = new Guid(idPrescribingInformation);
        IdUser = new Guid(idUser);
    }
    
}