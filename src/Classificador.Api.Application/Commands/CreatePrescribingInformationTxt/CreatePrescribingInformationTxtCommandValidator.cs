using Classificador.Api.Application.Core.Errors;
using Microsoft.AspNetCore.Http;

namespace Classificador.Api.Application.Commands.CreatePrescribingInformationTxt;

public sealed class CreatePrescribingInformationTxtCommandValidator : AbstractValidator<CreatePrescribingInformationTxtCommand>
{
    public CreatePrescribingInformationTxtCommandValidator()
    {
        RuleFor(x => x.File)
            .NotEmpty()
                .WithError(CommandErrors.CreatePrescribingInformationTxtFailures.FileIsRequired)
            .Must(BeAValidTextFile)
                .WithError(CommandErrors.CreatePrescribingInformationTxtFailures.FileExtension);
    }

    private static bool BeAValidTextFile(IFormFile? file)
    {
        if (file == null)
            return false;

        var contentTypeIsValid = file.ContentType.Equals("text/plain", StringComparison.OrdinalIgnoreCase);
        var extensionIsValid = Path.GetExtension(file.FileName).Equals(".txt", StringComparison.OrdinalIgnoreCase);

        return contentTypeIsValid && extensionIsValid;
    }
}