using System.Text;
using Classificador.Api.Domain.Core.Interfaces.Services;

namespace Classificador.Api.Infrastructure.Services;

public sealed class EmailSenderService : IEmailSenderService
{
    private readonly EmailOptions _options;
    private readonly ILogger<EmailSenderService> _logger;

    public EmailSenderService(IOptions<EmailOptions> options, ILogger<EmailSenderService> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public async Task<bool> SendEmailAsync(
        string emailForContact, 
        string contactName, 
        string messageSubject, 
        string messageBody, 
        CancellationToken cancellationToken = default)
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(emailForContact);
        ArgumentValidator.ThrowIfNullOrWhitespace(contactName);
        ArgumentValidator.ThrowIfNullOrWhitespace(messageSubject);
        ArgumentValidator.ThrowIfNullOrWhitespace(messageBody);

        try
        {
            MimeMessage email = new();
            CreateEmailBoxAddress(email);
            CreateEmailTexts(email, messageSubject, emailForContact, messageBody, contactName);
            await SendEmailBySmtpClientAsync(email, cancellationToken);

            return true;
        }
        catch(Exception ex)
        {
            _logger.LogInformation("Failed to Send Email: {ex}", ex);
            return false;
        }
    }

    private void CreateEmailBoxAddress(MimeMessage email)
    {
        email.From.Add(new MailboxAddress("Contato MedTagger", _options.EmailAddress));
        foreach (var emailAddress in _options.AdminsEmailAddress!)
        {
            email.To.Add(new MailboxAddress("Contato Admins", emailAddress));
        }
    }

    private static void CreateEmailTexts(MimeMessage email, string messageSubject, string emailForContact, string messageBody, string contactName)
    {
        email.Subject = messageSubject;

        StringBuilder sb = new();
        sb.AppendLine($"Contato de: {contactName} - {emailForContact} (enviado pelo site.)");
        sb.AppendLine(messageBody);
        sb.AppendLine($"\nData-Hora do Contato: {DateTime.UtcNow}.");

        email.Body = new TextPart("plain")
        {
            Text = sb.ToString()
        };
    }

    private async Task SendEmailBySmtpClientAsync(MimeMessage email, CancellationToken cancellationToken)
    {
        
        using var client = new SmtpClient();
        await client.ConnectAsync(_options.SmtpServer, _options.Port, false, cancellationToken);
        await client.AuthenticateAsync(_options.EmailAddress, _options.EmailPassword, cancellationToken);
        await client.SendAsync(email, cancellationToken);
        await client.DisconnectAsync(true, cancellationToken);
    }
}