using Classificador.Api.Application.Core.Errors;

namespace Classificador.Api.Application.Commands.SendEmailToContact;

public sealed class SendEmailToContactCommandValidator : AbstractValidator<SendEmailToContactCommand>
{
    public SendEmailToContactCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithError(CommandErrors.SendEmailToContact.NameIsRequired);
        
        RuleFor(x => x.Subject)
            .NotEmpty()
                .WithError(CommandErrors.SendEmailToContact.SubjectIsRequired);
        
        RuleFor(x => x.Email)
            .NotEmpty()
                .WithError(CommandErrors.SendEmailToContact.EmailIsRequired)
            .EmailAddress()
                .WithError(CommandErrors.SendEmailToContact.EmailFormat);;
        
        RuleFor(x => x.Message)
            .NotEmpty()
                .WithError(CommandErrors.SendEmailToContact.MessageIsRequired);

    }
}