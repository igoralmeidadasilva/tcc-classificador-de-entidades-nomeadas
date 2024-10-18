using Classificador.Api.Domain.Core.Errors;

namespace Classificador.Api.Application.Queries.GetPendingClassifications;

public sealed class GetPendingClassificationsQueryHandler : IRequestHandler<GetPendingClassificationsQuery, Result>
{
    private readonly ILogger<GetPendingClassificationsQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IClassificationReadOnlyRepository _classificationReadOnlyRepository;

    public GetPendingClassificationsQueryHandler(
        ILogger<GetPendingClassificationsQueryHandler> logger,
        IMapper mapper,
        IClassificationReadOnlyRepository classificationReadOnlyRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _classificationReadOnlyRepository = classificationReadOnlyRepository;
    }

    public async Task<Result> Handle(GetPendingClassificationsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Classification> pendingClassifications = await _classificationReadOnlyRepository
                .GetPendingClassificationsByPrescribingInformationAndIdUser(request.IdPrescribingInformation, request.IdUser, cancellationToken);

        if(pendingClassifications is null)
        {
            _logger.LogInformation("{RequestName} did not find any classification.",
                nameof(GetPendingClassificationsQuery));

            return  Result.Failure(DomainErrors.NamedEntity.NamedEntityNoneWereFound);
        }
        
        if(!pendingClassifications.Any())
        {
            _logger.LogInformation("{RequestName} Prescribing Information with Id {id} it has no classification pending.", 
                nameof(GetPendingClassificationsQuery),
                request.IdPrescribingInformation);
            
            return Result.Success(new List<ClassifyNamedEntityViewPendingClassificationDto>());
        }

        _logger.LogInformation("{RequestName} found {RecordsCount} pendings classifications records.",
            nameof(GetPendingClassificationsQuery),
            pendingClassifications.Count());

        List<ClassifyNamedEntityViewPendingClassificationDto> mappedPendingClassifications = 
            pendingClassifications.Select(_mapper.Map<ClassifyNamedEntityViewPendingClassificationDto>).ToList();

        return Result.Success(mappedPendingClassifications);
    }
}