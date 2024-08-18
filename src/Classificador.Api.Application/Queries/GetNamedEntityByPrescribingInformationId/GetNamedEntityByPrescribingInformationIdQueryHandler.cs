namespace Classificador.Api.Application.Queries.GetNamedEntityByPrescribingInformationId;

public sealed class GetNamedEntityByPrescribingInformationIdQueryHandler : IRequestHandler<GetNamedEntityByPrescribingInformationIdQuery, Result>
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

    public async Task<Result> Handle(GetNamedEntityByPrescribingInformationIdQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<NamedEntity> namedEntities = 
            await _namedEntityReadOnlyRepository.GetByPrescribingInformationIdAsync(request.PrescribingInformationId, cancellationToken);
    
        if(namedEntities == null)
        {
            _logger.LogInformation("{RequestName} did not find any named entities",
                nameof(GetNamedEntityByPrescribingInformationIdQuery));

            return  Result.Failure(DomainErrors.NamedEntity.NamedEntityNoneWereFound);
        }

        _logger.LogInformation("{RequestName} found {RecordsCount} named entities records",
            nameof(GetNamedEntityByPrescribingInformationIdQuery),
            namedEntities.Count());

        List<ClassifyNamedEntityViewNamedEntityDto> mapperNamedEntities = 
            namedEntities.Select(_mapper.Map<ClassifyNamedEntityViewNamedEntityDto>).ToList();
            
        return Result.Success(mapperNamedEntities);
    }

}