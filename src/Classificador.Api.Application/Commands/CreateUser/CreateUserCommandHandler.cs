using Classificador.Api.Domain.Core.Errors;
using Classificador.Api.Domain.Core.Interfaces.Services;

namespace Classificador.Api.Application.Commands.CreateUser;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result>
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
            _logger.LogInformation("{RequestName} user email already exists. {User}",
                nameof(CreateUserCommand),
                request.Email);

            return Result.Failure(DomainErrors.User.EmailAlreadyExists);
        }

        User newUser = User.Create(request.Email, request.Password, request.Name, request.IdSpecialty, request.Contact);
        newUser = newUser.UpdateHashedPassword(_passwordHashingService.HashPassword(newUser.HashedPassword));

        await _userPersistenceRepository.AddAsync(newUser, cancellationToken);

        _logger.LogInformation("{RequestName} successfully created a new user with id: {UserId}.",
            nameof(CreateUserCommand),
            newUser.Id);

        return Result.Success();
    }
}
