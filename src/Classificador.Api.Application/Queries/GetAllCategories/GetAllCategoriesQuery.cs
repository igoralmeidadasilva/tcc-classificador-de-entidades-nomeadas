using Classificador.Api.Application.Dtos;

namespace Classificador.Api.Application.Queries.GetAllCategories;

public sealed record GetAllCategoriesQuery : IQuery<Result<IEnumerable<ClassifyNamedEntityViewCategoryDto>>>
{ }