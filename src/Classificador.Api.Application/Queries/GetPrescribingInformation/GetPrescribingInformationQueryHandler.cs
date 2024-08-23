namespace Classificador.Api.Application.Queries.GetPrescribingInformation;

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
            await _prescribingInformationReadOnlyRepository.GetByNameOrDescriptionAsync(request.PrescribingInformationName!, cancellationToken);

        if(prescribingInformations is null)
        {
            _logger.LogInformation("{RequestName} did not find any prescribing informations",
                nameof(GetPrescribingInformationQuery));

            return  Result.Failure(DomainErrors.PrescribingInformation.PrescribingInformationEntityNotFound);
        }

        _logger.LogInformation("{RequestName} found {RecordsCount} prescribing informations records",
            nameof(GetPrescribingInformationQuery),
            prescribingInformations.Count());

        List<ChoosePrescribingInformationViewDto> mapperPrescribingInformations = (await Task.WhenAll
        (
            prescribingInformations.Select(async pri =>
            {
                ChoosePrescribingInformationViewDto dto = _mapper.Map<ChoosePrescribingInformationViewDto>(pri);
                int count = await _classificationReadOnlyRepository.GetCountClassificationByUserId(request.IdUser, pri.Id, cancellationToken);
                dto = dto with { UserAmount = count };

                return dto;
            })
        )).ToList();

        return Result.Success(mapperPrescribingInformations);
    }
}