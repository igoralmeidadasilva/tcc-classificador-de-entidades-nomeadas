using Classificador.Api.Application.Core.Errors;
using Classificador.Api.Domain;

namespace Classificador.Api.Application.Commands.CreateSpecialty;

public sealed class CreateSpecialtyCommandValidator : AbstractValidator<CreateSpecialtyCommand>
{
    public CreateSpecialtyCommandValidator()
    {
        RuleFor(x => x.Name)
        .NotEmpty()
            .WithError(CommandErrors.CreateSpecialtyFailures.NameIsRequired)
        .MaximumLength(Constants.Constraints.Specialty.NAME_MAX_LENGHT)
            .WithError(CommandErrors.CreateSpecialtyFailures.NameMaximumLenght);
    }
}