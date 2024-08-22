namespace Classificador.Api.Application.Commands.DeletePendingClassification;

public sealed record DeletePendingClassificationCommand : IQuery<Result>
{
    public Guid IdClassification { get; init; }
}