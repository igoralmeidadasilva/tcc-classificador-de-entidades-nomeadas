using Classificador.Api.Application.Dtos;
using Classificador.Api.Domain.Core.Errors;

namespace Classificador.Api.Application.Queries.GetCategories;

public sealed class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, Result<GetCategoriesQueryResponse>>
{
    private readonly ILogger<GetCategoriesQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;

    public GetCategoriesQueryHandler(ILogger<GetCategoriesQueryHandler> logger, IMapper mapper, ICategoryReadOnlyRepository categoryReadOnlyRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _categoryReadOnlyRepository = categoryReadOnlyRepository;
    }

    public async Task<Result<GetCategoriesQueryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Category> categories = await _categoryReadOnlyRepository.GetAllAsync(cancellationToken);

        if(!categories.Any() || categories is null)
        {
            _logger.LogInformation("{RequestName} did not find any categories.",
                nameof(GetCategoriesQuery));

            return Result.Failure<GetCategoriesQueryResponse>(DomainErrors.Category.CategoryEntityNoneWereFound);
        }

        _logger.LogInformation("{RequestName} found {RecordsCount} categories records.",
            nameof(GetCategoriesQuery),
            categories.Count());

        IEnumerable<ClassifyNamedEntityViewCategoryDto> mapperCategories = categories.Select(_mapper.Map<ClassifyNamedEntityViewCategoryDto>);
        
        return Result.Success(new GetCategoriesQueryResponse { Response = mapperCategories });
    }
}