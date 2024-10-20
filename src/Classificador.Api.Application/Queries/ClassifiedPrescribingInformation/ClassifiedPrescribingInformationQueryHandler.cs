using Classificador.Api.Application.Dtos;
using Classificador.Api.Domain.Core.Errors;

namespace Classificador.Api.Application.Queries.ClassifiedPrescribingInformation;

public sealed class ClassifiedPrescribingInformationQueryHandler
    : IQueryHandler<ClassifiedPrescribingInformationQuery, Result<IEnumerable<PrescribingInformationClassifiedDto>>>
{
    private readonly ILogger<ClassifiedPrescribingInformationQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPrescribingInformationReadOnlyRepository _prescribingInformationReadOnlyRepository;
    private readonly IClassificationReadOnlyRepository _classificationReadOnlyRepository;

    public ClassifiedPrescribingInformationQueryHandler(
        ILogger<ClassifiedPrescribingInformationQueryHandler> logger,
        IMapper mapper,
        IPrescribingInformationReadOnlyRepository prescribingInformationReadOnlyRepository,
        IClassificationReadOnlyRepository classificationReadOnlyRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _prescribingInformationReadOnlyRepository = prescribingInformationReadOnlyRepository;
        _classificationReadOnlyRepository = classificationReadOnlyRepository;
    }

    public async Task<Result<IEnumerable<PrescribingInformationClassifiedDto>>> Handle(
        ClassifiedPrescribingInformationQuery request, 
        CancellationToken cancellationToken)
    {
        IEnumerable<PrescribingInformation> prescribingInformations =
            await _prescribingInformationReadOnlyRepository.GetAllAsync(cancellationToken);

        if(prescribingInformations is null)
        {
            _logger.LogInformation("{RequestName} did not find any prescribing informations",
                nameof(ClassifiedPrescribingInformationQuery));

            return Result.Failure<IEnumerable<PrescribingInformationClassifiedDto>>(DomainErrors.PrescribingInformation.PrescribingInformationEntityNoneWereFound);
        }

        _logger.LogInformation("{RequestName} found {RecordsCount} prescribing informations records.",
            nameof(ClassifiedPrescribingInformationQuery),
            prescribingInformations.Count());

        IEnumerable<PrescribingInformationClassifiedDto> mapperPrescribingInformations =
            prescribingInformations.Select(_mapper.Map<PrescribingInformationClassifiedDto>).ToList();

        return Result.Success(mapperPrescribingInformations);
    }
}