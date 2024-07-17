namespace Classificador.Api.Application.Queries.CountingVotesForNamedEntity;

public sealed class CountingVotesForNamedEntityQueryHandler : IRequestHandler<CountingVotesForNamedEntityQuery, Result>
{
    private readonly ILogger<CountingVotesForNamedEntityQueryHandler> _logger;
    private readonly IClassificationReadOnlyRepository _classificationReadOnlyRepository;
    private readonly INamedEntityReadOnlyRepository _namedEntityReadOnlyRepository;

    public CountingVotesForNamedEntityQueryHandler(
        ILogger<CountingVotesForNamedEntityQueryHandler> logger,
        IClassificationReadOnlyRepository classificationReadOnlyRepository,
        INamedEntityReadOnlyRepository namedEntityReadOnlyRepository)
    {
        _logger = logger;
        _classificationReadOnlyRepository = classificationReadOnlyRepository;
        _namedEntityReadOnlyRepository = namedEntityReadOnlyRepository;
    }


    public async Task<Result> Handle(CountingVotesForNamedEntityQuery request, CancellationToken cancellationToken)
    {
        if (!await _namedEntityReadOnlyRepository.ExistsAsync(request.IdNamedEntity, cancellationToken))
        {
            _logger.LogInformation("{RequestName} the named entity searched for does not exist {IdNamedEntity}",
                nameof(CountingVotesForNamedEntityQuery),
                request.IdNamedEntity);
            return Result.Failure(DomainErrors.NamedEntity.NamedEntityNotFound);
        }

        IEnumerable<CountVoteForNamedEntity> response = 
            await _classificationReadOnlyRepository.GetCountingVotesForNamedEntityAsync(request.IdNamedEntity, cancellationToken);

            _logger.LogInformation("{RequestName} successfully fechting for named entity votes. Amount records: {Count}",
                nameof(CountingVotesForNamedEntityQuery),
                response.Count());

        return Result.Success(response);
    }

}
