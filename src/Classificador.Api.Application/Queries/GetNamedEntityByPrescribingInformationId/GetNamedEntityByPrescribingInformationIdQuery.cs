namespace Classificador.Api.Application.Queries.GetNamedEntityByPrescribingInformationId;

public sealed record GetNamedEntityByPrescribingInformationIdQuery : IQuery<Result>
{
    public Guid PrescribingInformationId { get; set; }

    public GetNamedEntityByPrescribingInformationIdQuery(string prescribingInformationId)
    {
        PrescribingInformationId = new Guid(prescribingInformationId);
    }
    
}