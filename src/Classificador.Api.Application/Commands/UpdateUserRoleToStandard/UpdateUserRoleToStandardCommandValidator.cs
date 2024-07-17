namespace Classificador.Api.Application.Commands.UpdateUserRoleToStandard;

public sealed class UpdateUserRoleToStandardCommandValidator : AbstractValidator<UpdateUserRoleToStandardCommand>
{
    public UpdateUserRoleToStandardCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithError(CommandErrors.UpdateUserRoleFailures.UserIdIsRequired);
    }
}
