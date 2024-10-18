using Classificador.Api.SharedKernel.Shared.Extensions;

namespace Classificador.Api.Application.Commands.CreateSpecialty;

public sealed record CreateSpecialtyCommand : ICommand<Result>
{
    public string Name { get; init; }
    public string? Description { get; init; }

    public CreateSpecialtyCommand(string name, string description)
    {
        Name = name.RemoveAccents().ToUpperInvariant();
        Description = description ?? string.Empty;
    }
}