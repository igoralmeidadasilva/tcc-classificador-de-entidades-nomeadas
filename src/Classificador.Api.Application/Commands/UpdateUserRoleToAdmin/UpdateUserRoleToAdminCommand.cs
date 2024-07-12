namespace Classificador.Api.Application.Commands.UpdateUserRoleToAdmin;

public sealed record UpdateUserRoleToAdminCommand : ICommand<Result>
{
    public Guid Id { get; init;}
}
