using System.Security.Claims;
using Classificador.Api.Domain.Interfaces.Services;
using Classificador.Api.Domain.Models;

namespace Classificador.Api.Application.Commands.LoginUser;

public sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result>
{
    private readonly ILogger<LoginUserCommandHandler> _logger;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly ITokenService _tokenService;

    public LoginUserCommandHandler(
        ILogger<LoginUserCommandHandler> logger,
        IUserReadOnlyRepository userReadOnlyRepository,
        IPasswordHashingService passwordHashingService,
        ITokenService tokenService)
    {
        _logger = logger;
        _userReadOnlyRepository = userReadOnlyRepository;
        _passwordHashingService = passwordHashingService;
        _tokenService = tokenService;
    }

    public async Task<Result> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _userReadOnlyRepository.GetByEmailAsync(request.Email!);

        if(user is null)
        {
            return Result.Failure(DomainErrors.User.UserNotFound);
        }

        if (!_passwordHashingService.VerifyPassword(user.HashedPassword, request.Password!))
        {
            return Result.Failure(DomainErrors.User.AuthenticationPasswordFailed);
        }

        IEnumerable<Claim> claims = _tokenService.GenerateClaims(user);

        JwtToken token = _tokenService.GenerateToken(claims);
        
        return Result.Success(token);

    }

}
