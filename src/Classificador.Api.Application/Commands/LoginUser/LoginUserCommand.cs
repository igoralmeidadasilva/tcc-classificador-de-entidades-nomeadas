namespace Classificador.Api.Application.Commands.LoginUser;

public sealed record LoginUserCommand : ICommand<Result>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public LoginUserCommand(string email, string password)
    {
        Email = email is null ? string.Empty : email.ToLowerInvariant();
        Password = password ?? string.Empty;
    }

    public LoginUserCommand()
    { }
}
