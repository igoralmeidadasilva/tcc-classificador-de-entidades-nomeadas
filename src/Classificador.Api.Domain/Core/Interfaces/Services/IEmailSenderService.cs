namespace Classificador.Api.Domain.Core.Interfaces.Services;

public interface IEmailSenderService
{
    public Task<bool> SendEmailAsync(string from, string name, string subject, string body, CancellationToken cancellationToken = default);
}