namespace Classificador.Api.Application.Queries.GetAllClassificationByVotes;

public sealed class GetAllClassificationByVotesQueryHandler : IRequestHandler<GetAllClassificationByVotesQuery, Result>
{
    private readonly ILogger<GetAllClassificationByVotesQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IClassificationReadOnlyRepository _classificationReadOnlyRepository;
    private readonly INamedEntityReadOnlyRepository _namedEntityReadOnlyRepository;

    public GetAllClassificationByVotesQueryHandler(
        ILogger<GetAllClassificationByVotesQueryHandler> logger,
        IMapper mapper,
        IClassificationReadOnlyRepository classificationReadOnlyRepository,
        INamedEntityReadOnlyRepository namedEntityReadOnlyRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _classificationReadOnlyRepository = classificationReadOnlyRepository;
        _namedEntityReadOnlyRepository = namedEntityReadOnlyRepository;
    }

    public async Task<Result> Handle(GetAllClassificationByVotesQuery request, CancellationToken cancellationToken)
    {
        // FIXME: Melhorar esse handler, de preferencia jogar toda a manipulação de dados para o banco
        var namedEntities = await _namedEntityReadOnlyRepository.GetByPrescribingInformationAsync(request.IdPrescribingInformation, cancellationToken);
        List<CountVoteForNamedEntity> response = [];

        foreach(NamedEntity item in namedEntities)
        {
            var votes = await _classificationReadOnlyRepository.GetCountingVotesForNamedEntityAsync(item.Id, cancellationToken);
            if(votes.FirstOrDefault() != default)
            {
             response.Add(votes.FirstOrDefault()!);
            }
        }

        return Result.Success(response);
    }
}