using System.Security.Claims;

namespace Classificador.Api.Application.Commands.LoginUser;

public sealed record LoginUserCommand : ICommand<Result<LoginUserCommandResponse>>
{
    public string Email { get; init; }
    public string Password { get; init; }

    public LoginUserCommand(string email, string password)
    {
        Email = email is null ? string.Empty : email.ToLowerInvariant();
        Password = password ?? string.Empty;
    }

}
