using System.Text.RegularExpressions;

namespace Classificador.Api.Application.Queries.GetPrescribingInformation;

public sealed record GetPrescribingInformationQuery : IQuery<Result>
{
    public string? PrescribingInformationName { get; init; }

    public GetPrescribingInformationQuery(string? prescribingInformationName)
    {
        prescribingInformationName = prescribingInformationName?.Trim();
        prescribingInformationName = Regex.Replace(prescribingInformationName!, @"[^\w\s]", string.Empty);
        prescribingInformationName = prescribingInformationName?.ToLower();

        PrescribingInformationName = prescribingInformationName;
    }
}