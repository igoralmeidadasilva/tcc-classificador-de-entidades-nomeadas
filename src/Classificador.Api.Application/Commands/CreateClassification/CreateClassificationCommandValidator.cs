namespace Classificador.Api.Application.Commands.CreateClassification;

public sealed class CreateClassificationCommandValidator : AbstractValidator<CreateClassificationCommand>
{
    public CreateClassificationCommandValidator()
    {
        RuleFor(x => x.IdUser)
            .NotNull()
                .WithError(CommandErrors.CreateClassificationFailures.IdUserIsRequired)
            .NotEqual(Guid.Empty)
                .WithError(CommandErrors.CreateClassificationFailures.IdUserIsRequired);

        RuleFor(x => x.IdNamedEntity)
            .NotNull()
                .WithError(CommandErrors.CreateClassificationFailures.IdNamedEntityIsRequired)
            .NotEqual(Guid.Empty)
                .WithError(CommandErrors.CreateClassificationFailures.IdNamedEntityIsRequired);;

        RuleFor(x => x.IdCategory)
            .NotNull()
                .WithError(CommandErrors.CreateClassificationFailures.IdCategoryIsRequired)
            .NotEqual(Guid.Empty)
                .WithError(CommandErrors.CreateClassificationFailures.IdCategoryIsRequired);;

    }
    
}