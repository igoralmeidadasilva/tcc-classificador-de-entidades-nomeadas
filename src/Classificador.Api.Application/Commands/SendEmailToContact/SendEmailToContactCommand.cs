namespace Classificador.Api.Application.Commands.SendEmailToContact;

public sealed record SendEmailToContactCommand : ICommand<Result>
{
    public string Name { get; init; } = string.Empty;
    public string Subject { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;

    public SendEmailToContactCommand(string name, string subject, string email, string message)
    {
        Name = name;
        Subject = subject;
        Email = email.ToLowerInvariant();
        Message = message;
    }

    public SendEmailToContactCommand() 
    { }

}