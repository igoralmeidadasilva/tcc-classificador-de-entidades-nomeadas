using Classificador.Api.Application.Commands.CreateSpecialty;
using Classificador.Api.Application.Dtos;

namespace Classificador.Api.Application.Profiles;

public sealed class SpecialtyProfile : Profile
{
    public SpecialtyProfile()
    {
        CreateMap<CreateSpecialtyCommand, Specialty>();
        CreateMap<SpecialtySignUpViewDto, Specialty>().ReverseMap();
    }
}