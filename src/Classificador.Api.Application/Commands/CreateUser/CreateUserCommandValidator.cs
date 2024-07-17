namespace Classificador.Api.Application.Commands.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
                .WithError(CommandErrors.CreateUserFailures.EmailIsRequired)
            .MaximumLength(Constants.Constraints.USER_EMAIL_MAX_LENGHT)
                .WithError(CommandErrors.CreateUserFailures.EmailMaximumLenght)
            .EmailAddress()
                .WithError(CommandErrors.CreateUserFailures.EmailFormat);

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithError(CommandErrors.CreateUserFailures.PasswordIsRequired)
            .MinimumLength(Constants.Constraints.USER_PASSWORD_MIN_LENGHT)
                .WithError(CommandErrors.CreateUserFailures.PasswordMinimumLenght)
            .MaximumLength(Constants.Constraints.USER_PASSWORD_MAX_LENGHT)
                .WithError(CommandErrors.CreateUserFailures.PasswordMaximumLenght)
            .Must(x => x.Any(value => char.IsUpper(value)))
                .WithError(CommandErrors.CreateUserFailures.PasswordFormatInvalidUpperCase)
            .Must(x => x.Any(value => char.IsLower(value)))
                .WithError(CommandErrors.CreateUserFailures.PasswordFormatInvalidLowerCase)
            .Must(x => x.Any(value => char.IsDigit(value)))
                .WithError(CommandErrors.CreateUserFailures.PasswordFormatInvalidNumber)
            .Matches("(?=.*[@#$%^&+=])")
                .WithError(CommandErrors.CreateUserFailures.PasswordFormatInvalidNonAlphanumeric);

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
                .WithError(CommandErrors.CreateUserFailures.PasswordIsRequired)
            .MinimumLength(Constants.Constraints.USER_PASSWORD_MIN_LENGHT)
                .WithError(CommandErrors.CreateUserFailures.ConfirmPasswordMinimumLenght)
            .MaximumLength(Constants.Constraints.USER_PASSWORD_MAX_LENGHT)
                .WithError(CommandErrors.CreateUserFailures.ConfirmPasswordMaximumLenght)
            .Must(x => x.Any(value => char.IsUpper(value)))
                .WithError(CommandErrors.CreateUserFailures.ConfirmPasswordFormatInvalidUpperCase)
            .Must(x => x.Any(value => char.IsLower(value)))
                .WithError(CommandErrors.CreateUserFailures.ConfirmPasswordFormatInvalidLowerCase)
            .Must(x => x.Any(value => char.IsDigit(value)))
                .WithError(CommandErrors.CreateUserFailures.ConfirmPasswordFormatInvalidNumber)
            .Matches("(?=.*[@#$%^&+=])")
                .WithError(CommandErrors.CreateUserFailures.ConfirmPasswordFormatInvalidNonAlphanumeric)
            .Equal(x => x.Password)
                .WithError(CommandErrors.CreateUserFailures.PasswordsNotEquals);

        RuleFor(x => x.Name)
            .NotEmpty()
                .WithError(CommandErrors.CreateUserFailures.NameIsRequired)
            .MaximumLength(Constants.Constraints.USER_FIRST_NAME_MAX_LENGHT)
                .WithError(CommandErrors.CreateUserFailures.NameMaximumLenght);

        RuleFor(x => x.Contact)
            .MaximumLength(Constants.Constraints.USER_CONTACT_MAX_LENGHT)
                .WithError(CommandErrors.CreateUserFailures.ContactMaximumLenght)
            .Matches(@"^\(\d{2}\)\d{5}-\d{4}$")
                .WithError(CommandErrors.CreateUserFailures.ContactFormat)
                .When(x => x.Contact != string.Empty);
    }

}
