using System.Text;
using Classificador.Api.Domain.Core.Errors;
using Classificador.Api.Domain.Models;

namespace Classificador.Api.Application.Queries.DownloadSpacyModel;

public sealed class DownloadSpacyModelQueryHandler : IQueryHandler<DownloadSpacyModelQuery, Result<DownloadSpacyModelQueryResponse>>
{
    private readonly ILogger<DownloadSpacyModelQueryHandler> _logger;
    private readonly IClassificationReadOnlyRepository _classificationReadOnlyRepository;
    private readonly IPrescribingInformationReadOnlyRepository _prescribingInformationReadOnlyRepository;

    public DownloadSpacyModelQueryHandler(
        ILogger<DownloadSpacyModelQueryHandler> logger,
        IClassificationReadOnlyRepository classificationReadOnlyRepository,
        IPrescribingInformationReadOnlyRepository prescribingInformationReadOnlyRepository)
    {
        _logger = logger;
        _classificationReadOnlyRepository = classificationReadOnlyRepository;
        _prescribingInformationReadOnlyRepository = prescribingInformationReadOnlyRepository;
    }

    public async Task<Result<DownloadSpacyModelQueryResponse>> Handle(DownloadSpacyModelQuery request, CancellationToken cancellationToken)
    {
        PrescribingInformation? prescribingInformation =
            await _prescribingInformationReadOnlyRepository.GetByIdAsync(request.IdPrescribingInformation, cancellationToken);

        if (prescribingInformation is null)
        {
            _logger.LogInformation("{RequestName} Prescribing information does not exist",
                nameof(DownloadSpacyModelQuery));

            return Result.Failure<DownloadSpacyModelQueryResponse>(DomainErrors.PrescribingInformation.PrescribingInformationEntityNoneWereFound);
        }

        IEnumerable<CountVoteForNamedEntity> classificationsByVotes = await _classificationReadOnlyRepository
            .GetMostVotedEntityByPrescribingInformation(request.IdPrescribingInformation, cancellationToken);

        List<SpacyNerModel> spacyNerModelList = [];
        StringBuilder textStringBuilder = new();
        foreach (var classification in classificationsByVotes)
        {
            textStringBuilder
                .Append(classification.Entity)
                .Append(Environment.NewLine);

            var spacyNerModel = new SpacyNerModel
            {
                Start = classification.Start,
                End = classification.End,
                Label = classification.Category
            };

            spacyNerModelList.Add(spacyNerModel);
        }

        var GetDownloadSpacyModelQueryResponse = new DownloadSpacyModelQueryResponse
        {
            Name = prescribingInformation.Name,
            Text = textStringBuilder.ToString(),
            Entities = spacyNerModelList
        };

        return Result.Success(GetDownloadSpacyModelQueryResponse);
    }

}
