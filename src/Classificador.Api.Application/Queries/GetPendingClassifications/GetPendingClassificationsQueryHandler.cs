using Classificador.Api.Application.Dtos;
using Classificador.Api.Domain.Core.Errors;

namespace Classificador.Api.Application.Queries.GetPendingClassifications;

public sealed class GetPendingClassificationsQueryHandler 
    : IQueryHandler<GetPendingClassificationsQuery, Result<IEnumerable<ClassifyNamedEntityViewPendingClassificationDto>>>
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

    public async Task<Result<IEnumerable<ClassifyNamedEntityViewPendingClassificationDto>>> Handle(
        GetPendingClassificationsQuery request, 
        CancellationToken cancellationToken)
    {
        IEnumerable<Classification> pendingClassifications = await _classificationReadOnlyRepository
                .GetPendingClassificationsByPrescribingInformationAndIdUser(request.IdPrescribingInformation, request.IdUser, cancellationToken);

        if(pendingClassifications is null)
        {
            _logger.LogInformation("{RequestName} did not find any classification.",
                nameof(GetPendingClassificationsQuery));

            return  Result.Failure<IEnumerable<ClassifyNamedEntityViewPendingClassificationDto>>(DomainErrors.NamedEntity.NamedEntityNoneWereFound);
        }
        
        if(!pendingClassifications.Any())
        {
            _logger.LogInformation("{RequestName} Prescribing Information with Id {id} it has no classification pending.", 
                nameof(GetPendingClassificationsQuery),
                request.IdPrescribingInformation);
            
            return Result.Success(Enumerable.Empty<ClassifyNamedEntityViewPendingClassificationDto>());
        }

        _logger.LogInformation("{RequestName} found {RecordsCount} pendings classifications records.",
            nameof(GetPendingClassificationsQuery),
            pendingClassifications.Count());

        IEnumerable<ClassifyNamedEntityViewPendingClassificationDto> mappedPendingClassifications = 
            pendingClassifications.Select(_mapper.Map<ClassifyNamedEntityViewPendingClassificationDto>).ToList();

        return Result.Success(mappedPendingClassifications);
    }
}