namespace Classificador.Api.Application.Commands.UpdateClassificationToCompleted;

public sealed class UpdateClassificationToCompletedCommandValidator : AbstractValidator<UpdateClassificationToCompletedCommand>
{
    public UpdateClassificationToCompletedCommandValidator()
    {
        RuleFor(x => x.IdPrescribingInformation)
            .NotEmpty()
                .WithError(CommandErrors.UpdateClassificationToCompletedFailures.PrescribingInformationIdIsRequired);

        RuleFor(x => x.IdUser)
            .NotEmpty()
                .WithError(CommandErrors.UpdateClassificationToCompletedFailures.UserIdIsRequired);
    }
}