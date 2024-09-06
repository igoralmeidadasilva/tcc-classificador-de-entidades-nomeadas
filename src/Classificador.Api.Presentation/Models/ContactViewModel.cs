namespace Classificador.Api.Presentation.Models;

// TODO: Refatorar essa ViewModel para usar Forms constomizados e deixar de carregar o email pela viewBag
public sealed class ContactViewModel
{
    public string EmailToContact { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    public static implicit operator SendEmailToContactCommand(ContactViewModel viewModel) =>
        new(viewModel.Name, viewModel.Subject, viewModel.Email, viewModel.Message);

}