using Classificador.Api.Application.Dtos;
using Classificador.Api.Domain.Core.Errors;

namespace Classificador.Api.Application.Queries.GetNamedEntityByPrescribingInformationId;

public sealed class GetNamedEntityByPrescribingInformationIdQueryHandler 
    : IQueryHandler<GetNamedEntityByPrescribingInformationIdQuery, Result<IEnumerable<ClassifyNamedEntityViewNamedEntityDto>>>
{
    private readonly ILogger<GetNamedEntityByPrescribingInformationIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly INamedEntityReadOnlyRepository _namedEntityReadOnlyRepository;

    public GetNamedEntityByPrescribingInformationIdQueryHandler(
        ILogger<GetNamedEntityByPrescribingInformationIdQueryHandler> logger,
        IMapper mapper,
        INamedEntityReadOnlyRepository namedEntityReadOnlyRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _namedEntityReadOnlyRepository = namedEntityReadOnlyRepository;
    }

    public async Task<Result<IEnumerable<ClassifyNamedEntityViewNamedEntityDto>>> Handle(
        GetNamedEntityByPrescribingInformationIdQuery request, 
        CancellationToken cancellationToken)
    {
        IEnumerable<NamedEntity> namedEntities = 
            await _namedEntityReadOnlyRepository.GetByPrescribingInformationAndUserAsync(request.IdPrescribingInformation, request.IdUser, cancellationToken);
    
        if(namedEntities is null)
        {
            _logger.LogInformation("{RequestName} did not find any named entities",
                nameof(GetNamedEntityByPrescribingInformationIdQuery));

            return  Result.Failure<IEnumerable<ClassifyNamedEntityViewNamedEntityDto>>(DomainErrors.NamedEntity.NamedEntityNoneWereFound);
        }

        if(!namedEntities.Any())
        {
            _logger.LogInformation("{RequestName} Prescribing Information with Id {id} it has no named entities to classify.",
                nameof(GetNamedEntityByPrescribingInformationIdQuery),
                request.IdPrescribingInformation);

            return Result.Success(Enumerable.Empty<ClassifyNamedEntityViewNamedEntityDto>());
        }

        _logger.LogInformation("{RequestName} found {RecordsCount} named entities records",
            nameof(GetNamedEntityByPrescribingInformationIdQuery),
            namedEntities.Count());

        IEnumerable<ClassifyNamedEntityViewNamedEntityDto> mapperNamedEntities = 
            namedEntities.Select(_mapper.Map<ClassifyNamedEntityViewNamedEntityDto>).ToList();
            
        return Result.Success(mapperNamedEntities);
    }
}