namespace Classificador.Api.Application.Commands.SendEmailToContact;

public sealed record SendEmailToContactCommand : ICommand<Result>
{
    public string Name { get; init; } = string.Empty;
    public string Subject { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;

    public SendEmailToContactCommand(string name, string subject, string email, string message)
    {
        Name = name ?? string.Empty;
        Subject = subject ?? string.Empty;
        Email = email is null ? string.Empty : email.ToLowerInvariant();
        Message = message ?? string.Empty;
    }
}