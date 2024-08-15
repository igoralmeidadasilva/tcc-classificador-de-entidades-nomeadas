namespace Classificador.Api.Application.Queries.GetPrescribingInformation;

public sealed class GetPrescribingInformationQueryHandler : IRequestHandler<GetPrescribingInformationQuery, Result>
{
    private readonly ILogger<GetPrescribingInformationQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPrescribingInformationReadOnlyRepository _prescribingInformationReadOnlyRepository;

    public GetPrescribingInformationQueryHandler(
        ILogger<GetPrescribingInformationQueryHandler> logger,
        IMapper mapper,
        IPrescribingInformationReadOnlyRepository prescribingInformationReadOnlyRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _prescribingInformationReadOnlyRepository = prescribingInformationReadOnlyRepository;
    }

    public async Task<Result> Handle(GetPrescribingInformationQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<PrescribingInformation> prescribingInformations = 
            await _prescribingInformationReadOnlyRepository.GetByNameOrDescriptionAsync(request.PrescribingInformationName!, cancellationToken);

        if(prescribingInformations == null)
        {
            _logger.LogInformation("{RequestName} did not find any prescribing informations",
                nameof(GetPrescribingInformationQuery));
            return  Result.Failure(DomainErrors.PrescribingInformation.PrescribingInformationEntityNotFound);
        }

        _logger.LogInformation("{RequestName} found {RecordsCount} prescribing informations records",
            nameof(GetPrescribingInformationQuery),
            prescribingInformations.Count());

        IEnumerable<ChoosePrescribingInformationViewDto> mapperPrescribingInformations = 
            prescribingInformations.Select(_mapper.Map<ChoosePrescribingInformationViewDto>);
            
        return Result.Success(mapperPrescribingInformations);
    }
}