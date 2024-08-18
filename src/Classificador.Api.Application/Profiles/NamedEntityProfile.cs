using System.Globalization;

namespace Classificador.Api.Application.Profiles;

public sealed class NamedEntityProfile : Profile
{
    public NamedEntityProfile()
    {
        CreateMap<ClassifyNamedEntityViewNamedEntityDto, NamedEntity>()
            .ReverseMap()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => FormatNamedEntityName(src.Name)));
    }

    private static string FormatNamedEntityName(string name)
    {
        StringBuilder sb = new();
        sb = sb.Append(name);
        
        sb.Replace("-", " ");
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(sb.ToString().ToLower());
    }
}