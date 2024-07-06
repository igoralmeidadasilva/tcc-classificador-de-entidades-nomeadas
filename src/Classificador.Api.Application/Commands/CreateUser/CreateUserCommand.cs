namespace Classificador.Api.Application.Commands.CreateUser;

public sealed record CreateUserCommand : ICommand<Unit>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string ConfirmPassword { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? Contact { get; init; } = string.Empty;
}
