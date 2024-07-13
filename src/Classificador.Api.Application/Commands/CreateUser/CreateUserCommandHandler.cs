namespace Classificador.Api.Application.Commands.CreateUser;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
{
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly IUserPersistenceRepository _userPersistenceRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(
        ILogger<CreateUserCommandHandler> logger,
        IUserPersistenceRepository userPersistenceRepository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IPasswordHashingService passwordHashingService,
        IMapper mapper)
    {
        _logger = logger;
        _userPersistenceRepository = userPersistenceRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _passwordHashingService = passwordHashingService;
        _mapper = mapper;

    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if(await _userReadOnlyRepository.IsEmailAlreadyExists(request.Email, cancellationToken))
        {
            return Result.Failure(DomainErrors.User.EmailAlreadyExists);
        }

        User user = _mapper.Map<User>(request with 
        { 
            Password = _passwordHashingService.HashPassword(request.Password)
        });

        Guid id = await _userPersistenceRepository.AddAsync(user, cancellationToken);

        return Result.Success(id);
    }
}
