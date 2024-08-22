namespace Classificador.Api.Application.Queries.GetAllClassificationByVotes;

public sealed class GetAllClassificationByVotesQueryHandler : IRequestHandler<GetAllClassificationByVotesQuery, Result>
{
    private readonly ILogger<GetAllClassificationByVotesQueryHandler> _logger;
    private readonly IClassificationReadOnlyRepository _classificationReadOnlyRepository;
    private readonly IPrescribingInformationReadOnlyRepository _prescribingInformationReadOnlyRepository;

    public GetAllClassificationByVotesQueryHandler(
        ILogger<GetAllClassificationByVotesQueryHandler> logger,
        IClassificationReadOnlyRepository classificationReadOnlyRepository,
        IPrescribingInformationReadOnlyRepository prescribingInformationReadOnlyRepository)
    {
        _logger = logger;
        _classificationReadOnlyRepository = classificationReadOnlyRepository;
        _prescribingInformationReadOnlyRepository = prescribingInformationReadOnlyRepository;
    }

    public async Task<Result> Handle(GetAllClassificationByVotesQuery request, CancellationToken cancellationToken)
    {
        if(!await _prescribingInformationReadOnlyRepository.ExistsAsync(request.IdPrescribingInformation, cancellationToken))
        {
            _logger.LogInformation("{RequestName} Prescribing information does not exist",
                nameof(GetAllClassificationByVotesQuery));

            return Result.Failure(DomainErrors.PrescribingInformation.PrescribingInformationEntityNotFound);
        }

        IEnumerable<CountVoteForNamedEntity> response = await _classificationReadOnlyRepository
            .GetMostVotedEntityByPrescribingInformation(request.IdPrescribingInformation, cancellationToken);

        if(response is null)
        {
            _logger.LogInformation("{RequestName} error to find classifications.",
                nameof(GetAllClassificationByVotesQuery));

            return Result.Failure(DomainErrors.Classification.ClassificationsCompletedNotFound);
        }

        if(!response.Any())
        {
            _logger.LogInformation("{RequestName} did not find any classification.",
                nameof(GetAllClassificationByVotesQuery));
            
            return Result.Success(new List<CountVoteForNamedEntity>());
        }

        _logger.LogInformation("{RequestName} successfully fechting for named entity votes. Amount records: {Count}",
            nameof(GetAllClassificationByVotesQuery),
            response.Count());

        return Result.Success(response.ToList());
    }
}