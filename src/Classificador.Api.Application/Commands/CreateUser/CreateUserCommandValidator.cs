using Classificador.Api.Application.Core.Errors;
using Classificador.Api.Domain;

namespace Classificador.Api.Application.Commands.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
                .WithError(CommandErrors.CreateUserFailures.EmailIsRequired)
            .MaximumLength(Constants.Constraints.User.EMAIL_MAX_LENGHT)
                .WithError(CommandErrors.CreateUserFailures.EmailMaximumLenght)
            .EmailAddress()
                .WithError(CommandErrors.CreateUserFailures.EmailFormat);

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithError(CommandErrors.CreateUserFailures.PasswordIsRequired)
            .MinimumLength(Constants.Constraints.User.PASSWORD_MIN_LENGHT)
                .WithError(CommandErrors.CreateUserFailures.PasswordMinimumLenght)
            .MaximumLength(Constants.Constraints.User.PASSWORD_MAX_LENGHT)
                .WithError(CommandErrors.CreateUserFailures.PasswordMaximumLenght)
            .Must(x => x.Any(value => char.IsUpper(value)))
                .WithError(CommandErrors.CreateUserFailures.PasswordFormatInvalidUpperCase)
            .Must(x => x.Any(value => char.IsLower(value)))
                .WithError(CommandErrors.CreateUserFailures.PasswordFormatInvalidLowerCase)
            .Must(x => x.Any(value => char.IsDigit(value)))
                .WithError(CommandErrors.CreateUserFailures.PasswordFormatInvalidNumber)
            .Matches(Constants.Constraints.User.PASSWORD_FORMAT)
                .WithError(CommandErrors.CreateUserFailures.PasswordFormatInvalidNonAlphanumeric);

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
                .WithError(CommandErrors.CreateUserFailures.PasswordIsRequired)
            .MinimumLength(Constants.Constraints.User.PASSWORD_MIN_LENGHT)
                .WithError(CommandErrors.CreateUserFailures.ConfirmPasswordMinimumLenght)
            .MaximumLength(Constants.Constraints.User.PASSWORD_MAX_LENGHT)
                .WithError(CommandErrors.CreateUserFailures.ConfirmPasswordMaximumLenght)
            .Must(x => x.Any(value => char.IsUpper(value)))
                .WithError(CommandErrors.CreateUserFailures.ConfirmPasswordFormatInvalidUpperCase)
            .Must(x => x.Any(value => char.IsLower(value)))
                .WithError(CommandErrors.CreateUserFailures.ConfirmPasswordFormatInvalidLowerCase)
            .Must(x => x.Any(value => char.IsDigit(value)))
                .WithError(CommandErrors.CreateUserFailures.ConfirmPasswordFormatInvalidNumber)
            .Matches(Constants.Constraints.User.PASSWORD_FORMAT)
                .WithError(CommandErrors.CreateUserFailures.ConfirmPasswordFormatInvalidNonAlphanumeric)
            .Equal(x => x.Password)
                .WithError(CommandErrors.CreateUserFailures.PasswordsNotEquals);

        RuleFor(x => x.Name)
            .NotEmpty()
                .WithError(CommandErrors.CreateUserFailures.NameIsRequired)
            .MaximumLength(Constants.Constraints.User.NAME_MAX_LENGHT)
                .WithError(CommandErrors.CreateUserFailures.NameMaximumLenght);

        RuleFor(x => x.Contact)
            .MaximumLength(Constants.Constraints.User.CONTACT_MAX_LENGHT)
                .WithError(CommandErrors.CreateUserFailures.ContactMaximumLenght)
            .Matches(Constants.Constraints.User.CONTACT_FORMAT)
                .WithError(CommandErrors.CreateUserFailures.ContactFormat)
                .When(x => x.Contact != string.Empty);

        RuleFor(x => x.IdSpecialty)
            .NotEmpty()
                .WithError(CommandErrors.CreateUserFailures.SpecialtyIsRequired)
            .NotEqual(x => Guid.Empty)
                .WithError(CommandErrors.CreateUserFailures.SpecialtyIsRequired);
    }

}
