using Classificador.Api.Domain.Core.Errors;

namespace Classificador.Api.Application.Commands.UpdateClassificationToCompleted;

public sealed class UpdateClassificationToCompletedCommandHandler : IRequestHandler<UpdateClassificationToCompletedCommand, Result>
{
    private readonly ILogger<UpdateClassificationToCompletedCommandHandler> _logger;
    private readonly IClassificationPersistenceRepository _classificationPersistenceRepository;
    private readonly IClassificationReadOnlyRepository _classificationReadOnlyRepository;

    public UpdateClassificationToCompletedCommandHandler(
        ILogger<UpdateClassificationToCompletedCommandHandler> logger,
        IClassificationPersistenceRepository classificationPersistenceRepository,
        IClassificationReadOnlyRepository classificationReadOnlyRepository)
    {
        _logger = logger;
        _classificationPersistenceRepository = classificationPersistenceRepository;
        _classificationReadOnlyRepository = classificationReadOnlyRepository;
    }

    public async Task<Result> Handle(UpdateClassificationToCompletedCommand request, CancellationToken cancellationToken)
    {
        var pendingClassifications = await _classificationReadOnlyRepository
            .GetPendingClassificationsByPrescribingInformationAndIdUser(request.IdPrescribingInformation, request.IdUser, cancellationToken);

        if (!pendingClassifications.Any())
        {
            _logger.LogInformation("{RequestName} pending classifications could not be found",
                nameof(UpdateClassificationToCompletedCommand));
            return Result.Failure(DomainErrors.Classification.ClassificationsPendingNotFound);
        }

        _logger.LogInformation("{RequestName} found {CountRecords} pending records",
            nameof(UpdateClassificationToCompletedCommand),
            pendingClassifications.Count());

        foreach (var item in pendingClassifications)
        {
            await _classificationPersistenceRepository.UpdateStatusToCompletedAsync(item.Id, cancellationToken);
        }

        _logger.LogInformation("{RequestName} {CountRecords} classifications had their status updated",
            nameof(UpdateClassificationToCompletedCommand),
            pendingClassifications.Count());

        return Result.Success();
    }
}