namespace Classificador.Api.Application.Commands.UpdateClassificationToCompletedCommand;

public sealed class UpdateClassificationToCompletedCommandValidator : AbstractValidator<UpdateClassificationToCompletedCommand>
{
    public UpdateClassificationToCompletedCommandValidator()
    {
        RuleFor(x => x.IdPrescribingInformation)
            .NotEmpty()
                .WithError(CommandErrors.UpdateUserRoleFailures.UserIdIsRequired);

        RuleFor(x => x.IdUser)
            .NotEmpty()
                .WithError(CommandErrors.UpdateUserRoleFailures.UserIdIsRequired);
    }
}