using Classificador.Api.Domain.Core.Errors;

namespace Classificador.Api.Application.Commands.CreateClassification;

public sealed class CreateClassificationCommandHandler : ICommandHandler<CreateClassificationCommand, Result>
{
    private readonly ILogger<CreateClassificationCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly INamedEntityReadOnlyRepository _namedEntityReadOnlyRepository;
    private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;
    private readonly IClassificationPersistenceRepository _classificationPersistenceRepository;
    private readonly IClassificationReadOnlyRepository _classificationReadOnlyRepository;

    public CreateClassificationCommandHandler(
        ILogger<CreateClassificationCommandHandler> logger,
        IMapper mapper,
        IUserReadOnlyRepository userReadOnlyRepository,
        INamedEntityReadOnlyRepository namedEntityReadOnlyRepository,
        ICategoryReadOnlyRepository categoryReadOnlyRepository,
        IClassificationPersistenceRepository classificationPersistenceRepository,
        IClassificationReadOnlyRepository classificationReadOnlyRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _userReadOnlyRepository = userReadOnlyRepository;
        _namedEntityReadOnlyRepository = namedEntityReadOnlyRepository;
        _categoryReadOnlyRepository = categoryReadOnlyRepository;
        _classificationPersistenceRepository = classificationPersistenceRepository;
        _classificationReadOnlyRepository = classificationReadOnlyRepository;
    }


    public async Task<Result> Handle(CreateClassificationCommand request, CancellationToken cancellationToken)
    {
        Result entitesExists = await VerifyEntitiesExistis(request, cancellationToken);

        if(!entitesExists.IsSuccess)
        {
            _logger.LogInformation("{RequestName} some record does not exist {Result}",
                nameof(CreateClassificationCommand),
                entitesExists.FirstError());
            return entitesExists;
        }

        Classification classification = _mapper.Map<Classification>(request);

        await _classificationPersistenceRepository.AddAsync(classification, cancellationToken);

        _logger.LogInformation("{RequestName} successfully created a new classification.",
            nameof(CreateClassificationCommand));

        return Result.Success();
    }

    private async Task<Result> VerifyEntitiesExistis(CreateClassificationCommand request, CancellationToken cancellationToken)
    {
        if(!await _userReadOnlyRepository.ExistsAsync(request.IdUser, cancellationToken))
            return Result.Failure(DomainErrors.User.UserNotFound);
        
        if(!await _namedEntityReadOnlyRepository.ExistsAsync(request.IdNamedEntity, cancellationToken))
          return Result.Failure(DomainErrors.NamedEntity.NamedEntityNotFound);
        
        if(!await _categoryReadOnlyRepository.ExistsAsync(request.IdCategory, cancellationToken))
            return Result.Failure(DomainErrors.Category.CategoryEntityNotFound);

        if(await _classificationReadOnlyRepository.VerifyIfClassificationExistsAsync(request.IdNamedEntity, request.IdUser, cancellationToken))
            return Result.Failure(DomainErrors.Classification.ClassificationAlreadyExists);

        return Result.Success();
    }
}