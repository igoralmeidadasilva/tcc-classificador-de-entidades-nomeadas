using Classificador.Api.Domain.Core.Errors;

namespace Classificador.Api.Application.Queries.GetGetPrescribingInformation;

public sealed class GetPrescribingInformationQueryHandler : IRequestHandler<GetPrescribingInformationQuery, Result>
{
    private readonly ILogger<GetPrescribingInformationQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPrescribingInformationReadOnlyRepository _prescribingInformationReadOnlyRepository;
    private readonly IClassificationReadOnlyRepository _classificationReadOnlyRepository;

    public GetPrescribingInformationQueryHandler(
        ILogger<GetPrescribingInformationQueryHandler> logger,
        IMapper mapper,
        IPrescribingInformationReadOnlyRepository prescribingInformationReadOnlyRepository,
        IClassificationReadOnlyRepository classificationReadOnlyRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _prescribingInformationReadOnlyRepository = prescribingInformationReadOnlyRepository;
        _classificationReadOnlyRepository = classificationReadOnlyRepository;
    }

    public async Task<Result> Handle(GetPrescribingInformationQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<PrescribingInformation> prescribingInformations = 
            await _prescribingInformationReadOnlyRepository.GetAllAsync(cancellationToken);

        if(prescribingInformations is null)
        {
            _logger.LogInformation("{RequestName} did not find any prescribing informations",
                nameof(GetPrescribingInformationQuery));

            return Result.Failure(DomainErrors.PrescribingInformation.PrescribingInformationEntityNotFound);
        }

        _logger.LogInformation("{RequestName} found {RecordsCount} prescribing informations records.",
            nameof(GetPrescribingInformationQuery),
            prescribingInformations.Count());
            
        List<PrescribingInformationClassificationViewDto> mapperPrescribingInformations = 
            prescribingInformations.Select(_mapper.Map<PrescribingInformationClassificationViewDto>).ToList();

        return Result.Success(mapperPrescribingInformations);
    }
}