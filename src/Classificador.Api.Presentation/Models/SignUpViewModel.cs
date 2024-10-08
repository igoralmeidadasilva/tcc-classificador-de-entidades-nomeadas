namespace Classificador.Api.Presentation.Models;

public sealed record SignUpViewModel
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string ConfirmPassword { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? Contact { get; init; }
    public SelectList? Specialties { get; set; }
    public Guid? SpecialtyId { get; set; }

    public static implicit operator CreateUserCommand(SignUpViewModel viewModel) =>
        new(viewModel.Email, viewModel.Password, viewModel.ConfirmPassword, viewModel.Name, viewModel.Contact, viewModel.SpecialtyId);
}