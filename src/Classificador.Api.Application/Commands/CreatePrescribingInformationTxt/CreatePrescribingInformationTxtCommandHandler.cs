namespace Classificador.Api.Application.Commands.CreatePrescribingInformationTxt;

public sealed class CreatePrescribingInformationTxtCommandHandler : IRequestHandler<CreatePrescribingInformationTxtCommand, Result>
{
    private readonly ILogger<CreatePrescribingInformationTxtCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPrescribingInformationPersistenceRepository _prescribingInformationPersistenceRepository;

    public CreatePrescribingInformationTxtCommandHandler(
        ILogger<CreatePrescribingInformationTxtCommandHandler> logger,
        IMapper mapper,
        IPrescribingInformationPersistenceRepository prescribingInformationPersistenceRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _prescribingInformationPersistenceRepository = prescribingInformationPersistenceRepository;
    }

    public async Task<Result> Handle(CreatePrescribingInformationTxtCommand request, CancellationToken cancellationToken)
    {
        PrescribingInformation prescribingInformation = _mapper.Map<PrescribingInformation>(request);

        List<NamedEntity> namedEntities = ExtractNamedEntities(prescribingInformation);

        _logger.LogInformation("{RequestName} found {EntityCount} {NamedEntity} Found in the {PrescribingInformation} {PrescribingInformationName}.",
            nameof(CreatePrescribingInformationTxtCommand),
            namedEntities.Count,
            nameof(NamedEntity),
            nameof(PrescribingInformation),
            prescribingInformation.Name);

        prescribingInformation.NamedEntities = namedEntities;

        Guid id = await _prescribingInformationPersistenceRepository.AddAsync(prescribingInformation, cancellationToken);

        _logger.LogInformation("{RequestName} successfully created a new PrescribingInformation: {PrescribingInformationId}",
            nameof(CreatePrescribingInformationTxtCommand),
            id);

        return Result.Success(id);
    }

    private static List<NamedEntity> ExtractNamedEntities(PrescribingInformation prescribingInformation)
    {
        List<string> namedEntitiesName = prescribingInformation.Text
            .Trim()
            .Split('\n')
            .Distinct()
            .ToList();

        List<NamedEntity> namedEntities = 
            namedEntitiesName.Select(entityNamed =>
            {
                int startPosition = entityNamed.IndexOf(entityNamed) + 1;
                int endPosition = startPosition + (entityNamed.Length - 1);
                WordPosition position = WordPosition.Create(startPosition, endPosition);
                return new NamedEntity
                (
                    entityNamed, 
                    $"Entidades extraidas da bula {prescribingInformation.Name}",
                    position
                );
            })
            .ToList();

        return namedEntities;
    }

}