using Classificador.Api.Domain.Core.Errors;
using Classificador.Api.Domain.Core.Interfaces.Services;

namespace Classificador.Api.Application.Commands.SendEmailToAdmins;

public sealed class SendEmailToAdminsCommandHandler : ICommandHandler<SendEmailToAdminsCommand, Result>
{
    private readonly ILogger<SendEmailToAdminsCommandHandler> _logger;
    private readonly IEmailSenderService _emailSenderService;

    public SendEmailToAdminsCommandHandler(ILogger<SendEmailToAdminsCommandHandler> logger, IEmailSenderService emailSenderService)
    {
        _logger = logger;
        _emailSenderService = emailSenderService;
    }

    public async Task<Result> Handle(SendEmailToAdminsCommand request, CancellationToken cancellationToken)
    {
        bool isSend = await _emailSenderService.SendEmailAsync(
            request.EmailForContact!.ToLowerInvariant(), 
            request.ContactName!, 
            request.MessageSubject!, 
            request.MessageBody!, 
            cancellationToken);

        if (!isSend)
        {
            _logger.LogInformation("{RequestName} Email cannot be found. {UserEmail}",
                nameof(SendEmailToAdminsCommand),
                request.EmailForContact);

            return Result.Failure(DomainErrors.EmailSend.FailedToSendEmail);
        }

        _logger.LogInformation("{RequestName} was successful, an email was sent from {UserEmail} with the subject {EmailSubject}.",
            nameof(SendEmailToAdminsCommand),
            request.EmailForContact,
            request.MessageSubject);

        return Result.Success();
    }
}