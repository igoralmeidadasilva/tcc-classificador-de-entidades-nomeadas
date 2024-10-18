using Classificador.Api.Application.Commands.CreateCategory;
using Classificador.Api.Application.Dtos;

namespace Classificador.Api.Application.Profiles;

public sealed class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CreateCategoryCommand, Category>();

        CreateMap<ClassifyNamedEntityViewCategoryDto, Category>()
            .ReverseMap();
    }
}