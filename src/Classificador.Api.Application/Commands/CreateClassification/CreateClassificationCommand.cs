namespace Classificador.Api.Application.Commands.CreateClassification;

public sealed record CreateClassificationCommand : ICommand<Result>
{
    public Guid IdUser { get; init; }
    public Guid IdNamedEntity { get; init; }
    public Guid IdCategory { get; init; }
    public string? Comment { get; init; }
}
