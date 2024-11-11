namespace Classificador.Api.Application.Commands.DeletePendingClassification;

public sealed record DeletePendingClassificationCommand : ICommand<Result>
{
    public Guid IdClassification { get; init; }

    public DeletePendingClassificationCommand(Guid idClassification)
    {
        IdClassification = idClassification;
    }
}