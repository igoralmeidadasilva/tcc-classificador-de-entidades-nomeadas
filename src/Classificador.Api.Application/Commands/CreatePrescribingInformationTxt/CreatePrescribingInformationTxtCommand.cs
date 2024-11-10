using Microsoft.AspNetCore.Http;

namespace Classificador.Api.Application.Commands.CreatePrescribingInformationTxt;

public sealed record CreatePrescribingInformationTxtCommand : ICommand<Result>
{
    public IFormFile? File{ get; init; }
    public string? Description { get; set; } = string.Empty;
    public CreatePrescribingInformationTxtCommand(IFormFile? file, string? description)
    {
        File = file;
        Description = description;
    }
    
}