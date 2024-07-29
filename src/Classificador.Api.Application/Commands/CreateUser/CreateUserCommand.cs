namespace Classificador.Api.Application.Commands.CreateUser;

public sealed record CreateUserCommand : ICommand<Result>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string ConfirmPassword { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? Contact { get; init; }

    public CreateUserCommand(string email, string password, string confirmPassword, string name, string? contact)
    {
        Email = email.ToLowerInvariant();
        Password = password;
        ConfirmPassword = confirmPassword;
        Name = name;
        Contact = contact ?? string.Empty;
    }

    public CreateUserCommand()
    { }
}
