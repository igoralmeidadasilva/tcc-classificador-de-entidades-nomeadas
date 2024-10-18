using Classificador.Api.Application.Core.Errors;
using Classificador.Api.Domain;

namespace Classificador.Api.Application.Commands.CreateCategory;

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithError(CommandErrors.CreateCategoryFailures.NameIsRequired)
            .MaximumLength(Constants.Constraints.Category.NAME_MAX_LENGHT)
                .WithError(CommandErrors.CreateCategoryFailures.NameMaximumLenght);
    }
}