using Classificador.Api.Application.Core.Errors;

namespace Classificador.Api.Application.Commands.SendEmailToAdmins;

public sealed class SendEmailToAdminsCommandValidator : AbstractValidator<SendEmailToAdminsCommand>
{
    public SendEmailToAdminsCommandValidator()
    {
        RuleFor(x => x.ContactName)
            .NotEmpty()
                .WithError(CommandErrors.SendEmailToAdmins.NameIsRequired);

        RuleFor(x => x.MessageSubject)
            .NotEmpty()
                .WithError(CommandErrors.SendEmailToAdmins.SubjectIsRequired);

        RuleFor(x => x.EmailForContact)
            .NotEmpty()
                .WithError(CommandErrors.SendEmailToAdmins.EmailIsRequired)
            .EmailAddress()
                .WithError(CommandErrors.SendEmailToAdmins.EmailFormat); ;

        RuleFor(x => x.MessageBody)
            .NotEmpty()
                .WithError(CommandErrors.SendEmailToAdmins.MessageIsRequired);

    }
}