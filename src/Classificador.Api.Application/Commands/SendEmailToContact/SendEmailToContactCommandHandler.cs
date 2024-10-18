using Classificador.Api.Domain.Core.Errors;

namespace Classificador.Api.Application.Commands.SendEmailToContact;

public sealed class SendEmailToContactCommandHandler : IRequestHandler<SendEmailToContactCommand, Result>
{
    private readonly ILogger<SendEmailToContactCommandHandler> _logger;
    private readonly IEmailSenderService _emailSenderService;

    public SendEmailToContactCommandHandler(ILogger<SendEmailToContactCommandHandler> logger, IEmailSenderService emailSenderService)
    {
        _logger = logger;
        _emailSenderService = emailSenderService;
    }

    public async Task<Result> Handle(SendEmailToContactCommand request, CancellationToken cancellationToken)
    {
        bool isSend = await _emailSenderService.SendEmailAsync(request.Email, request.Name, request.Subject, request.Message, cancellationToken);

        if(!isSend)
        {
            _logger.LogInformation("{RequestName} Email cannot be found. {UserEmail}",
                nameof(SendEmailToContactCommand),
                request.Email);
            
            return Result.Failure(DomainErrors.EmailSend.FailedToSendEmail);
        }

        _logger.LogInformation("{RequestName} was successful, an email was sent from {UserEmail} with the subject {EmailSubject}.",
            nameof(SendEmailToContactCommand),
            request.Email,
            request.Subject);

        return Result.Success();
    }
}