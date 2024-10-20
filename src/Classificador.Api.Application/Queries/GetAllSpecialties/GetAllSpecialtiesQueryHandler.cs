using Classificador.Api.Application.Dtos;
using Classificador.Api.Domain.Core.Errors;

namespace Classificador.Api.Application.Queries.GetAllSpecialties;

public sealed class GetAllSpecialtiesQueryHandler : IQueryHandler<GetAllSpecialtiesQuery, Result<IEnumerable<SpecialtySignUpViewDto>>>
{
    private readonly ILogger<GetAllSpecialtiesQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ISpecialtyReadOnlyRepository _specialtyReadOnlyRepository;

    public GetAllSpecialtiesQueryHandler(ILogger<GetAllSpecialtiesQueryHandler> logger, IMapper mapper, ISpecialtyReadOnlyRepository specialtyReadOnlyRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _specialtyReadOnlyRepository = specialtyReadOnlyRepository;
    }

    public async Task<Result<IEnumerable<SpecialtySignUpViewDto>>> Handle(GetAllSpecialtiesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Specialty> specialties = await _specialtyReadOnlyRepository.GetAllAsync(cancellationToken);

        if (!specialties.Any() || specialties is null)
        {
            _logger.LogInformation("{RequestName} did not find any specialties.",
                nameof(GetAllSpecialtiesQuery));

            return Result.Failure<IEnumerable<SpecialtySignUpViewDto>>(DomainErrors.Specialty.SpecialtyEntityNoneWereFound);
        }

        _logger.LogInformation("{RequestName} found {RecordsCount} specialties records.",
            nameof(GetAllSpecialtiesQuery),
            specialties.Count());

        IEnumerable<SpecialtySignUpViewDto> mappedSpecialties = specialties.Select(_mapper.Map<SpecialtySignUpViewDto>);

        return Result.Success(mappedSpecialties);
    }

}
