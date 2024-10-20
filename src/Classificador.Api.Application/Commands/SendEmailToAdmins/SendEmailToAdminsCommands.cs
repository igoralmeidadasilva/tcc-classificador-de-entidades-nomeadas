namespace Classificador.Api.Application.Commands.SendEmailToAdmins;

public sealed record SendEmailToAdminsCommand : ICommand<Result>
{
    public string? ContactName { get; init; }
    public string? MessageSubject { get; init; }
    public string? EmailForContact { get; init; }
    public string? MessageBody { get; init; }
}