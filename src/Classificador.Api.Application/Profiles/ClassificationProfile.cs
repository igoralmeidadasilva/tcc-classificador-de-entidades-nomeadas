namespace Classificador.Api.Application.Profiles;

public sealed class ClassificationProfile : Profile
{
    public ClassificationProfile()
    {
        CreateMap<CreateClassificationCommand, Classification>()
            .ForMember(x => x.NamedEntity, opt => opt.Ignore())
            .ForMember(x => x.User, opt => opt.Ignore())
            .ForMember(x => x.Category, opt => opt.Ignore());
    }

}
