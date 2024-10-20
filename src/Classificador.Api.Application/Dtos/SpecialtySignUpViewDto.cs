namespace Classificador.Api.Application.Dtos;

public sealed record SpecialtySignUpViewDto
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
}
