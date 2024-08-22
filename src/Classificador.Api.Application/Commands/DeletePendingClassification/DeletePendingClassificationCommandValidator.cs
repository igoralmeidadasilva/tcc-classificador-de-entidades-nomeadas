namespace Classificador.Api.Application.Commands.DeletePendingClassification;

public sealed class DeletePendingClassificationCommandValidator : AbstractValidator<DeletePendingClassificationCommand>
{
    public DeletePendingClassificationCommandValidator()
    {
        RuleFor(x => x.IdClassification)
            .NotEmpty()
                .WithError(CommandErrors.DeletePendingClassificationFailures.ClassificationIdIsRequired);
    }
}