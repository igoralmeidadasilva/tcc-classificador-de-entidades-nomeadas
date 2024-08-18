namespace Classificador.Api.Application.Queries.GetNamedEntityByPrescribingInformationId;

public sealed record GetNamedEntityByPrescribingInformationIdQuery : IQuery<Result>
{
    public Guid IdPrescribingInformation { get; set; }

    public GetNamedEntityByPrescribingInformationIdQuery(string idPrescribingInformation)
    {
        IdPrescribingInformation = new Guid(idPrescribingInformation);
    }
    
}