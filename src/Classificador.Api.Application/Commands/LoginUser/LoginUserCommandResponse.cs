using System.Security.Claims;

namespace Classificador.Api.Application.Commands.LoginUser;

public sealed record LoginUserCommandResponse : ICommandResponse
{
    public ClaimsIdentity? Response { get; set; }
}
