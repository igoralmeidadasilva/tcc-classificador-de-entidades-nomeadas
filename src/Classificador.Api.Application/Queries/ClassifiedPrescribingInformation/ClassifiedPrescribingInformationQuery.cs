using Classificador.Api.Application.Dtos;

namespace Classificador.Api.Application.Queries.ClassifiedPrescribingInformation;

public sealed record ClassifiedPrescribingInformationQuery : IQuery<Result<IEnumerable<PrescribingInformationClassifiedDto>>>
{ }