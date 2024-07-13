namespace Classificador.Api.Application.Commands.LoginUser;

public sealed record LoginUserCommand : ICommand<Result>
{
    public string? Email { get; set; }
    public string? Password { get; init; }
}
