namespace Classificador.Api.Presentation.Models;

public sealed class SignUpViewModel
{
    public SelectList? Specialties { get; set; }
    public CreateUserForm? CreateUserForm { get; set; }
}

public sealed class CreateUserForm
{
    public string? Email { get; init; }
    public string? Password { get; init; }
    public string? ConfirmPassword { get; init; }
    public string? Name { get; init; }
    public string? Contact { get; init; }
    public Guid? SpecialtyId { get; set; }
}
