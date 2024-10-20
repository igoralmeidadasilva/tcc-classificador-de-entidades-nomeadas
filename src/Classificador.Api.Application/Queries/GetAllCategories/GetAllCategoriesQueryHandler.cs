using Classificador.Api.Application.Dtos;
using Classificador.Api.Domain.Core.Errors;

namespace Classificador.Api.Application.Queries.GetAllCategories;

public sealed class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, Result<GetAllCategoriesQueryResponse>>
{
    private readonly ILogger<GetAllCategoriesQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;

    public GetAllCategoriesQueryHandler(ILogger<GetAllCategoriesQueryHandler> logger, IMapper mapper, ICategoryReadOnlyRepository categoryReadOnlyRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _categoryReadOnlyRepository = categoryReadOnlyRepository;
    }

    public async Task<Result<GetAllCategoriesQueryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Category> categories = await _categoryReadOnlyRepository.GetAllAsync(cancellationToken);

        if (!categories.Any() || categories is null)
        {
            _logger.LogInformation("{RequestName} did not find any categories.",
                nameof(GetAllCategoriesQuery));

            return Result.Failure<GetAllCategoriesQueryResponse>(DomainErrors.Category.CategoryEntityNoneWereFound);
        }

        _logger.LogInformation("{RequestName} found {RecordsCount} categories records.",
            nameof(GetAllCategoriesQuery),
            categories.Count());

        IEnumerable<ClassifyNamedEntityViewCategoryDto> mapperCategories = categories.Select(_mapper.Map<ClassifyNamedEntityViewCategoryDto>);

        return Result.Success(new GetAllCategoriesQueryResponse { Response = mapperCategories });
    }
}