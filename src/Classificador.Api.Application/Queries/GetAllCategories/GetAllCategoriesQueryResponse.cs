using Classificador.Api.Application.Dtos;

namespace Classificador.Api.Application.Queries.GetAllCategories;

public sealed record GetAllCategoriesQueryResponse : IQueryResponse
{
    public IEnumerable<ClassifyNamedEntityViewCategoryDto> Response { get; init; } = [];

}
