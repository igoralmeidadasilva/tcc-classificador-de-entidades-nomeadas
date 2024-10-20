using System.Security.Claims;
using Classificador.Api.Domain.Core.Errors;
using Classificador.Api.Domain.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Classificador.Api.Application.Commands.LoginUser;

public sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, Result<LoginUserCommandResponse>>
{
    private readonly ILogger<LoginUserCommandHandler> _logger;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IPasswordHashingService _passwordHashingService;

    public LoginUserCommandHandler(
        ILogger<LoginUserCommandHandler> logger,
        IUserReadOnlyRepository userReadOnlyRepository,
        IPasswordHashingService passwordHashingService)
    {
        _logger = logger;
        _userReadOnlyRepository = userReadOnlyRepository;
        _passwordHashingService = passwordHashingService;
    }

    public async Task<Result<LoginUserCommandResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _userReadOnlyRepository.GetByEmailAsync(request.Email!, cancellationToken);

        if(user is null)
        {
            _logger.LogInformation("{RequestName} user email cannot be found. {UserEmail}",
                nameof(LoginUserCommand),
                request.Email);

            return Result.Failure<LoginUserCommandResponse>(DomainErrors.User.UserNotFound);
        }

        if (!_passwordHashingService.VerifyPassword(user.HashedPassword, request.Password!))
        {
            _logger.LogInformation("{RequestName} user password is incorrect. {UserEmail}",
                nameof(LoginUserCommand),
                request.Email);

            return Result.Failure<LoginUserCommandResponse>(DomainErrors.User.AuthenticationPasswordFailed);
        }

        IEnumerable<Claim> claims =
        [
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Email, user.Email),
            new (ClaimTypes.Name, user.Name),
            new (ClaimTypes.Role, user.Role.ToString())
        ];

        ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        
        _logger.LogInformation("{RequestName} user login is successfully. {UserEmail}",
            nameof(LoginUserCommand),
            user.Email);

        return Result.Success(new LoginUserCommandResponse { Response = claimsIdentity });
    }

}
