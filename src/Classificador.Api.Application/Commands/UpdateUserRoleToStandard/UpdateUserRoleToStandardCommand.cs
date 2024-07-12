namespace Classificador.Api.Application.Commands.UpdateUserRoleToStandard;

public sealed record UpdateUserRoleToStandardCommand : ICommand<Result>
{
    public Guid Id { get; init; } = Guid.Empty;
}
