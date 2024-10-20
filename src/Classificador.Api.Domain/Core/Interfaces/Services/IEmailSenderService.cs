namespace Classificador.Api.Domain.Core.Interfaces.Services;

public interface IEmailSenderService
{
    public Task<bool> SendEmailAsync(
        string emailForContact, string contactName, string messageSubject, string messageBody, CancellationToken cancellationToken = default);
}