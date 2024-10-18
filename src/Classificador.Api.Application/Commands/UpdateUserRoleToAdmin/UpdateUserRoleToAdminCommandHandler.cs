using Classificador.Api.Domain.Core.Errors;

namespace Classificador.Api.Application.Commands.UpdateUserRoleToAdmin;

public sealed class UpdateUserRoleToAdminCommandHandler : IRequestHandler<UpdateUserRoleToAdminCommand, Result>
{
    private readonly ILogger<UpdateUserRoleToAdminCommandHandler> _logger;
    private readonly IUserPersistenceRepository _userPersistenceRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public UpdateUserRoleToAdminCommandHandler(
        ILogger<UpdateUserRoleToAdminCommandHandler> logger,
        IUserPersistenceRepository userPersistenceRepository,
        IUserReadOnlyRepository userReadOnlyRepository)
    {
        _logger = logger;
        _userPersistenceRepository = userPersistenceRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
    }


    public async Task<Result> Handle(UpdateUserRoleToAdminCommand request, CancellationToken cancellationToken)
    {
        User user = await _userReadOnlyRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            _logger.LogInformation("{RequestName} user id cannot be found. {UserId}",
                nameof(UpdateUserRoleToAdminCommand),
                request.Id);

            return Result.Failure(DomainErrors.User.UserNotFound);
        }

        user = user.UpdateRole(Domain.Enums.UserRole.Admin);

        await _userPersistenceRepository.UpdateAsync(user, cancellationToken);

        _logger.LogInformation("{RequestName} was successful, user {UserName} is now has the administrator role.",
            nameof(UpdateUserRoleToAdminCommand),
            user.Name);
                
        return Result.Success();
    }

}
