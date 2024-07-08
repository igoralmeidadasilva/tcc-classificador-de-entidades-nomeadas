using AutoMapper;

namespace Classificador.Api.Application.Profiles;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserCommand, User>()
            .ForMember(dest => dest.HashedPassword, opt => opt.MapFrom(src => src.Password));
    }

}
