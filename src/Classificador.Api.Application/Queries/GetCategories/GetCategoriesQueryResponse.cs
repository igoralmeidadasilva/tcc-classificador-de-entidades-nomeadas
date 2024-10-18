using Classificador.Api.Application.Dtos;

namespace Classificador.Api.Application.Queries.GetCategories;

public sealed record GetCategoriesQueryResponse : IQueryResponse
{
    public IEnumerable<ClassifyNamedEntityViewCategoryDto> Response { get; init; } = [];

    public GetCategoriesQueryResponse() {}

}
