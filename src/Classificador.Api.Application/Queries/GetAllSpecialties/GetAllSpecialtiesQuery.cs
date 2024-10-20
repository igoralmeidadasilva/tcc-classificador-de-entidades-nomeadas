using Classificador.Api.Application.Dtos;

namespace Classificador.Api.Application.Queries.GetAllSpecialties;

public sealed record GetAllSpecialtiesQuery : IQuery<Result<IEnumerable<SpecialtySignUpViewDto>>>
{ }
