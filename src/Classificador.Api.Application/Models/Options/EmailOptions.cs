namespace Classificador.Api.Application.Models.Options;

public sealed record EmailOptions
{
    public string? SmtpServer { get; init; }
    public int Port { get; init; }
    public string? EmailAddress { get; init; }
    public string? EmailPassword { get; init; } = Environment.GetEnvironmentVariable("EmailOptions__EmailPassword");
}