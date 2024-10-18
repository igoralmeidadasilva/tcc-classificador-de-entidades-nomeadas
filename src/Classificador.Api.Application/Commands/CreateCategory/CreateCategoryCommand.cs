using Classificador.Api.SharedKernel.Shared.Extensions;

namespace Classificador.Api.Application.Commands.CreateCategory;

public sealed record CreateCategoryCommand : ICommand<Result>
{
    public string Name { get; init; }
    public string? Description { get; init; }

    public CreateCategoryCommand(string name, string description)
    {
        Name = name.RemoveAccents().ToUpperInvariant();
        Description = description ?? string.Empty;;
    }
    
}