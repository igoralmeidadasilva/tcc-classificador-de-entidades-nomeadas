namespace Classificador.Api.Application.Commands.CreateSpecialty;

public sealed class CreateSpecialtyCommandHandler : IRequestHandler<CreateSpecialtyCommand, Result>
{
    private readonly ILogger<CreateSpecialtyCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ISpecialtyReadOnlyRepository _specialtyReadOnlyRepository;
    private readonly ISpecialtyPersistenceRepository _specialtyPersistenceRepository;

    public CreateSpecialtyCommandHandler(
        ILogger<CreateSpecialtyCommandHandler> logger,
        IMapper mapper,
        ISpecialtyReadOnlyRepository specialtyReadOnlyRepository,
        ISpecialtyPersistenceRepository specialtyPersistenceRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _specialtyReadOnlyRepository = specialtyReadOnlyRepository;
        _specialtyPersistenceRepository = specialtyPersistenceRepository;
    }

    public async Task<Result> Handle(CreateSpecialtyCommand request, CancellationToken cancellationToken)
    {
        if(await _specialtyReadOnlyRepository.ExistsByNameAsync(request.Name, cancellationToken))
        {
            _logger.LogInformation("{RequestName} specialty with this name already exists. {Specialty}",
                nameof(CreateSpecialtyCommand),
                request.Name);
            return Result.Failure(DomainErrors.Specialty.NameAlredyExists);
        }

        Specialty specialty = _mapper.Map<Specialty>(request);

        Guid id = await _specialtyPersistenceRepository.AddAsync(specialty, cancellationToken);

        _logger.LogInformation("{RequestName} successfully created a new specialty: {SpecialtyId}",
            nameof(CreateSpecialtyCommand),
            id);

        return Result.Success(id);
    }
}