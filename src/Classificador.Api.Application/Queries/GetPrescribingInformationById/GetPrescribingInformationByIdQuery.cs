namespace Classificador.Api.Application.Queries.GetPrescribingInformationById;

public sealed record GetPrescribingInformationByIdQuery : IQuery<Result>
{
    public string? PrescribingInformationName { get; init; }
    public Guid IdUser { get; init; }

    public GetPrescribingInformationByIdQuery(string? prescribingInformationName, string idUser)
    {
        prescribingInformationName = prescribingInformationName?.Trim();
        prescribingInformationName = Regex.Replace(prescribingInformationName!, @"[^\w\s]", string.Empty);

        PrescribingInformationName = prescribingInformationName;
        IdUser = new Guid(idUser);
    }
}