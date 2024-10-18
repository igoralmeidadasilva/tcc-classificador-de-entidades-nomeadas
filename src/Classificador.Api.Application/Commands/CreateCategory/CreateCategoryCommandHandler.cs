using Classificador.Api.Domain.Core.Errors;

namespace Classificador.Api.Application.Commands.CreateCategory;

public sealed class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, Result>
{
    private readonly ILogger<CreateCategoryCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;
    private readonly ICategoryPersistenceRepository _categoryPersistenceRepository;

    public CreateCategoryCommandHandler(
        ILogger<CreateCategoryCommandHandler> logger,
        IMapper mapper,
        ICategoryReadOnlyRepository categoryReadOnlyRepository,
        ICategoryPersistenceRepository categoryPersistenceRepository)
    {
        _logger = logger;
        _categoryReadOnlyRepository = categoryReadOnlyRepository;
        _categoryPersistenceRepository = categoryPersistenceRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        if(await _categoryReadOnlyRepository.ExistsByNameAsync(request.Name, cancellationToken))
        {
            _logger.LogInformation("{RequestName} category with this name already exists. {Category}",
                nameof(CreateCategoryCommand),
                request.Name);
            return Result.Failure(DomainErrors.Category.NameAlredyExists);
        }

        Category category = _mapper.Map<Category>(request);

        Guid id = await _categoryPersistenceRepository.AddAsync(category, cancellationToken);

        _logger.LogInformation("{RequestName} successfully created a new category: {CategoryId}",
            nameof(CreateCategoryCommand),
            id);

        return Result.Success();
    }
}