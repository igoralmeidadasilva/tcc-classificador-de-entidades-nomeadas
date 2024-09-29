using Newtonsoft.Json;

namespace Classificador.Api.Application.Queries.GetDownloadSpacyModel;

public sealed class GetDownloadSpacyModelQueryHandler : IRequestHandler<GetDownloadSpacyModelQuery, Result>
{
    private readonly ILogger<GetDownloadSpacyModelQueryHandler> _logger;
    private readonly IClassificationReadOnlyRepository _classificationReadOnlyRepository;
    private readonly IPrescribingInformationReadOnlyRepository _prescribingInformationReadOnlyRepository;

    public GetDownloadSpacyModelQueryHandler(ILogger<GetDownloadSpacyModelQueryHandler> logger, IClassificationReadOnlyRepository classificationReadOnlyRepository, IPrescribingInformationReadOnlyRepository prescribingInformationReadOnlyRepository)
    {
        _logger = logger;
        _classificationReadOnlyRepository = classificationReadOnlyRepository;
        _prescribingInformationReadOnlyRepository = prescribingInformationReadOnlyRepository;
    }


    public async Task<Result> Handle(GetDownloadSpacyModelQuery request, CancellationToken cancellationToken)
    {
        PrescribingInformation? prescribingInformation = 
            await _prescribingInformationReadOnlyRepository.GetByIdAsync(request.IdPrescribingInformation, cancellationToken);
            
        if(prescribingInformation is null)
        {
            _logger.LogInformation("{RequestName} Prescribing information does not exist",
                nameof(GetDownloadSpacyModelQuery));

            return Result.Failure(DomainErrors.PrescribingInformation.PrescribingInformationEntityNotFound);
        }

        IEnumerable<CountVoteForNamedEntity> classificationsByVotes = await _classificationReadOnlyRepository
            .GetMostVotedEntityByPrescribingInformation(request.IdPrescribingInformation, cancellationToken);

        IEnumerable<SpacyNerModel> spacyNerModelList = classificationsByVotes.Select(e => 
        {
            return new SpacyNerModel
            {
                Start = e.Start,
                End = e.End,
                Label = e.Category
            };
        });

        var GetDownloadSpacyModelQueryResponse = new GetDownloadSpacyModelQueryResponse
        {
            Entities = spacyNerModelList,
            Text = prescribingInformation.Text
        };

        return Result.Success(GetDownloadSpacyModelQueryResponse);
    }
}
