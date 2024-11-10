using System.Text.RegularExpressions;
using Classificador.Api.Application.Dtos;

namespace Classificador.Api.Application.Queries.GetPrescribingInformationById;

public sealed record GetPrescribingInformationByIdQuery : IQuery<Result<IEnumerable<ChoosePrescribingInformationViewDto>>>
{
    public string? PrescribingInformationName { get; init; }
    public Guid IdUser { get; init; }

    public GetPrescribingInformationByIdQuery(string? prescribingInformationName, Guid idUser)
    {
        PrescribingInformationName = (prescribingInformationName is null) ? string.Empty : FormatPrescribingInformationName(prescribingInformationName);
        IdUser = idUser;
    }

    private string FormatPrescribingInformationName(string name)
    {
        string formattedName = name.Trim();
        formattedName = Regex.Replace(formattedName, @"[^\w\s]", string.Empty);
        return formattedName;
    }
}