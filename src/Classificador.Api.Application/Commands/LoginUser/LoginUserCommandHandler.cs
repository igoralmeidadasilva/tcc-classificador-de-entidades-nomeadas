namespace Classificador.Api.Application.Commands.LoginUser;

public sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result>
{
    private readonly ILogger<LoginUserCommandHandler> _logger;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly IJwtSecurityTokenService _tokenService;

    public LoginUserCommandHandler(
        ILogger<LoginUserCommandHandler> logger,
        IUserReadOnlyRepository userReadOnlyRepository,
        IPasswordHashingService passwordHashingService,
        IJwtSecurityTokenService tokenService)
    {
        _logger = logger;
        _userReadOnlyRepository = userReadOnlyRepository;
        _passwordHashingService = passwordHashingService;
        _tokenService = tokenService;
    }

    public async Task<Result> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _userReadOnlyRepository.GetByEmailAsync(request.Email!, cancellationToken);

        if(user is null)
        {
            _logger.LogInformation("{RequestName} user email cannot be found. {UserEmail}",
                nameof(LoginUserCommand),
                request.Email);

            return Result.Failure(DomainErrors.User.UserNotFound);
        }

        if (!_passwordHashingService.VerifyPassword(user.HashedPassword, request.Password!))
        {
            _logger.LogInformation("{RequestName} user password is incorrect. {UserEmail}",
                nameof(LoginUserCommand),
                request.Email);

            return Result.Failure(DomainErrors.User.AuthenticationPasswordFailed);
        }

        IEnumerable<Claim> claims = _tokenService.GenerateClaims(user);

        JwtToken token = _tokenService.GenerateToken(claims);
        
        _logger.LogInformation("{RequestName} user login is successfully. {UserEmail}",
            nameof(LoginUserCommand),
            user.Email);

        return Result.Success(token);

    }

}
