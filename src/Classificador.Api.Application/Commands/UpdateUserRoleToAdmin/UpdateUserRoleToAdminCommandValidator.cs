namespace Classificador.Api.Application.Commands.UpdateUserRoleToAdmin;

public sealed class UpdateUserRoleToAdminCommandValidator : AbstractValidator<UpdateUserRoleToAdminCommand>
{
    public UpdateUserRoleToAdminCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithError(CommandErrors.UpdateUserRoleFailures.UserIdIsRequired);
    }
}
