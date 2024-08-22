namespace Classificador.Api.Application.Commands.UpdateClassificationToCompleted;

public sealed record UpdateClassificationToCompletedCommand() : IQuery<Result>
{
    public Guid IdUser { get; init; }
    public Guid IdPrescribingInformation { get; init; }
}