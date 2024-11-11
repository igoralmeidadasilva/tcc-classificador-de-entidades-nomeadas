namespace Classificador.Api.Application.Commands.UpdateClassificationToCompleted;

public sealed record UpdateClassificationToCompletedCommand : ICommand<Result>
{
    public Guid IdUser { get; init; }
    public Guid IdPrescribingInformation { get; init; }
    public UpdateClassificationToCompletedCommand(Guid idUser, Guid idPrescribingInformation)
    {
        IdUser = idUser;
        IdPrescribingInformation = idPrescribingInformation;
    }
}