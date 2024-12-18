namespace Classificador.Api.Application.Commands.CreateUser;

public sealed record CreateUserCommand : ICommand<Result>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string ConfirmPassword { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? Contact { get; init; }
    public Guid? IdSpecialty { get; init; }

    public CreateUserCommand(string email, string password, string confirmPassword, string name, string? contact, Guid? idSpecialty)
    {
        Email = email is null ? string.Empty : email.ToLowerInvariant();
        Password = password ?? string.Empty;
        ConfirmPassword = confirmPassword ?? string.Empty;
        Name = name ?? string.Empty;
        Contact = contact ?? string.Empty;
        IdSpecialty = idSpecialty ?? Guid.Empty;
    }
}
