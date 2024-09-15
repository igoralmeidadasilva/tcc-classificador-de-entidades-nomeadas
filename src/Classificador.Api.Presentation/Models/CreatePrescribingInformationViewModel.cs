namespace Classificador.Api.Presentation.Models;

public sealed record CreatePrescribingInformationViewModel
{
    public IFormFile? File { get; set; }
    public string? Description { get; set; }

    public static implicit operator CreatePrescribingInformationTxtCommand(CreatePrescribingInformationViewModel viewModel)
    {
        return new()
        {
            File = viewModel.File,
            Description = viewModel.Description
        };
    }
}
