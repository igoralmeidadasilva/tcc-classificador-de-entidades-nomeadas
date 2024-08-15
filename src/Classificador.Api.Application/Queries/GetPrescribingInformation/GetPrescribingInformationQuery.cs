namespace Classificador.Api.Application.Queries.GetPrescribingInformation;

public sealed record GetPrescribingInformationQuery : IQuery<Result>
{
    public string? PrescribingInformationName { get; init; }
}