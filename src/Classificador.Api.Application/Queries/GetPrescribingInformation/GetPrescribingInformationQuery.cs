namespace Classificador.Api.Application.Queries.GetPrescribingInformation;

public sealed record GetPrescribingInformationQuery : IQuery<Result>
{
    public string? PrescribingInformationName { get; init; }
    public Guid IdUser { get; init; }

    public GetPrescribingInformationQuery(string? prescribingInformationName, string idUser)
    {
        prescribingInformationName = prescribingInformationName?.Trim();
        prescribingInformationName = Regex.Replace(prescribingInformationName!, @"[^\w\s]", string.Empty);

        PrescribingInformationName = prescribingInformationName;
        IdUser = new Guid(idUser);
    }
}