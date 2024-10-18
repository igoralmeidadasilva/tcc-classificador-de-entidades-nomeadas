using Classificador.Api.Application.Commands.CreateSpecialty;

namespace Classificador.Api.Application.Profiles;

public sealed class SpecialtyProfile : Profile
{
    public SpecialtyProfile()
    {
        CreateMap<CreateSpecialtyCommand, Specialty>();
    }
}