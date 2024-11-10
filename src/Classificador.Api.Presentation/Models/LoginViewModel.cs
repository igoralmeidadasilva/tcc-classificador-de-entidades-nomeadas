namespace Classificador.Api.Presentation.Models;

public sealed record LoginViewModel
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;    
}