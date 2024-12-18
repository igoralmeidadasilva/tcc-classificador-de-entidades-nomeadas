using Classificador.Api.Application.Commands.CreateClassification;
using Classificador.Api.Application.Dtos;

namespace Classificador.Api.Application.Profiles;

public sealed class ClassificationProfile : Profile
{
    public ClassificationProfile()
    {
        CreateMap<CreateClassificationCommand, Classification>()
            .ForMember(dest => dest.NamedEntity, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore());

        CreateMap<ClassifyNamedEntityViewPendingClassificationDto, Classification>()
            .ReverseMap()
            .ForMember(dest => dest.NamedEntity, opt => opt.MapFrom(src => src.NamedEntity!.Name))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category!.Name))
            .ForMember(dest => dest.IdClassification, opt => opt.MapFrom(src => src.Id));
    }

}
