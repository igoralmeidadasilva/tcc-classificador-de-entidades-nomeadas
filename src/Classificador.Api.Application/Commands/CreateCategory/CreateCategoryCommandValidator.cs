namespace Classificador.Api.Application.Commands.CreateCategory;

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithError(CommandErrors.CreateCategoryFailures.NameIsRequired)
            .MaximumLength(Constants.Constraints.CATEGORYS_NAME_MAX_LENGHT)
                .WithError(CommandErrors.CreateCategoryFailures.NameMaximumLenght);
    }
}