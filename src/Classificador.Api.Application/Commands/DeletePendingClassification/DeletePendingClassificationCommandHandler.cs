using Classificador.Api.Domain.Core.Errors;

namespace Classificador.Api.Application.Commands.DeletePendingClassification;

public sealed class DeletePendingClassificationCommandHandler : ICommandHandler<DeletePendingClassificationCommand, Result>
{
    private readonly ILogger<DeletePendingClassificationCommandHandler> _logger;
    private readonly IClassificationPersistenceRepository _classificationPersistenceRepository;
    private readonly IClassificationReadOnlyRepository _classificationReadOnlyRepository;

    public DeletePendingClassificationCommandHandler(
        ILogger<DeletePendingClassificationCommandHandler> logger,
        IClassificationPersistenceRepository classificationPersistenceRepository,
        IClassificationReadOnlyRepository classificationReadOnlyRepository)
    {
        _logger = logger;
        _classificationPersistenceRepository = classificationPersistenceRepository;
        _classificationReadOnlyRepository = classificationReadOnlyRepository;
    }

    public async Task<Result> Handle(DeletePendingClassificationCommand request, CancellationToken cancellationToken)
    {
        if(!await _classificationReadOnlyRepository.ExistsAsync(request.IdClassification, cancellationToken))
        {
            _logger.LogInformation("{RequestName} The classification does not exist. {Id}",
               nameof(DeletePendingClassificationCommand),
               request.IdClassification);

            return Result.Failure(DomainErrors.Classification.ClassificationsPendingNotFound);
        }

        await _classificationPersistenceRepository.DeleteAsync(request.IdClassification, cancellationToken);

        _logger.LogInformation("{RequestName} The classification {Id} has been successfully removed.",
               nameof(DeletePendingClassificationCommand),
               request.IdClassification);

       return Result.Success();
    }
}