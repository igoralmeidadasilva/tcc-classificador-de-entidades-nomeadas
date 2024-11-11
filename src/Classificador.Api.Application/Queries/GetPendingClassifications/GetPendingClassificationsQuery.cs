using Classificador.Api.Application.Dtos;

namespace Classificador.Api.Application.Queries.GetPendingClassifications;

public sealed record GetPendingClassificationsQuery : IQuery<Result<IEnumerable<ClassifyNamedEntityViewPendingClassificationDto>>>
{
    public Guid IdUser { get; init; }
    public Guid IdPrescribingInformation { get; init; }
    public GetPendingClassificationsQuery(Guid idUser, Guid idPrescribingInformation)
    {
        IdUser = idUser;
        IdPrescribingInformation = idPrescribingInformation;
    }
}