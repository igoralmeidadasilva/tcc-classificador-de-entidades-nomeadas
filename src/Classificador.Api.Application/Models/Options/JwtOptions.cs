namespace Classificador.Api.Application.Models.Options;

public sealed record JwtOptions
{
    public string? TokenSecurityKey { get; init; }
    public int TokenExpirationInMinutes { get; init; }
}
