using Classificador.Api.Domain.Core.Errors;

namespace Classificador.Api.Application.Commands.UpdateUserRoleToStandard;

public sealed class UpdateUserRoleToStandardCommandHandler : IRequestHandler<UpdateUserRoleToStandardCommand, Result>
{
    private readonly ILogger<UpdateUserRoleToStandardCommandHandler> _logger;
    private readonly IUserPersistenceRepository _userPersistenceRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public UpdateUserRoleToStandardCommandHandler(
        ILogger<UpdateUserRoleToStandardCommandHandler> logger,
        IUserPersistenceRepository userPersistenceRepository,
        IUserReadOnlyRepository userReadOnlyRepository)
    {
        _logger = logger;
        _userPersistenceRepository = userPersistenceRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
    }


    public async Task<Result> Handle(UpdateUserRoleToStandardCommand request, CancellationToken cancellationToken)
    {
        User user = await _userReadOnlyRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            _logger.LogInformation("{RequestName} user id cannot be found. {UserId}",
                nameof(UpdateUserRoleToStandardCommand),
                request.Id);

            return Result.Failure(DomainErrors.User.UserNotFound);
        }

        user = user.UpdateRole(Domain.Enums.UserRole.Padrao);

        await _userPersistenceRepository.UpdateAsync(user, cancellationToken);

        _logger.LogInformation("{RequestName} was successful, user {UserName} is now has the standard role.",
            nameof(UpdateUserRoleToStandardCommand),
            user.Name);

        return Result.Success();
    }
}
