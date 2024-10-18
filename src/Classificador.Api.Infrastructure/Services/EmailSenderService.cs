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

    public async Task<bool> SendEmailAsync(string from, string name, string subject, string body, CancellationToken cancellationToken = default)
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(from);
        ArgumentValidator.ThrowIfNullOrWhitespace(name);
        ArgumentValidator.ThrowIfNullOrWhitespace(subject);
        ArgumentValidator.ThrowIfNullOrWhitespace(body);

        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("MedTagger Contato", from));
            message.To.Add(new MailboxAddress(name, _options.EmailAddress));
            message.Subject = $"From: {from} - {subject}";
            message.Body = new TextPart("plain")
            {
                Text = $"Nome: {name} \r\n{body}"
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_options.SmtpServer, _options.Port, false, cancellationToken);
            await client.AuthenticateAsync(_options.EmailAddress, _options.EmailPassword, cancellationToken);
            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);

            return true;
        }
        catch(Exception ex)
        {
            _logger.LogInformation("Failed to Send Email: {ex}", ex);
            return false;
        }
    }
}