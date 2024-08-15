namespace Classificador.Api.Presentation.Models;

public sealed record ContactViewModel
{
    public string Name { get; init; } = string.Empty;
    public string Subject { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;

    public static implicit operator SendEmailToContactCommand(ContactViewModel viewModel) =>
        new(viewModel.Name, viewModel.Subject, viewModel.Email, viewModel.Message);

}