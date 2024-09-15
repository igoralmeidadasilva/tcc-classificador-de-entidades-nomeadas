namespace Classificador.Api.Application.Profiles;

public sealed class PrescribingInformationProfile : Profile
{
    public PrescribingInformationProfile()
    {
        CreateMap<CreatePrescribingInformationTxtCommand, PrescribingInformation>()
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => ReadPrescribingInformationText(src.File!)))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => RemoveFileExtensionName(src.File!.FileName)));

        CreateMap<ChoosePrescribingInformationViewDto, PrescribingInformation>()
            .ReverseMap()
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.NamedEntities!.Count()));

        CreateMap<ChoosePrescribingInformationViewDto, PrescribingInformation>()
            .ReverseMap()
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.NamedEntities!.Count()));
        
        CreateMap<PrescribingInformationClassificationViewDto, PrescribingInformation>()
            .ReverseMap()
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.NamedEntities!.Count()));
    }

    private static string ReadPrescribingInformationText(IFormFile file)
    {
        var stringBuilder = new StringBuilder();
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            while(reader.Peek() >= 0)
            {
                stringBuilder.Append(reader.ReadLine());
                stringBuilder.Append('\n');
            }
        };
        return stringBuilder.ToString();
    }

    private static string RemoveFileExtensionName(string fileName)
    {
        return Path.GetFileNameWithoutExtension(fileName);
    }

}
