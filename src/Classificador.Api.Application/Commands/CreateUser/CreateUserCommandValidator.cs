namespace Classificador.Api.Application.Commands.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
                .WithError(RequestValidationErrors.CreateUserFailures.EmailIsRequired)
            .MaximumLength(Constants.Constraints.USER_EMAIL_MAX_LENGHT)
                .WithError(RequestValidationErrors.CreateUserFailures.EmailMaximumLenght)
            .EmailAddress()
                .WithError(RequestValidationErrors.CreateUserFailures.EmailFormat);

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithError(RequestValidationErrors.CreateUserFailures.PasswordIsRequired)
            .MinimumLength(Constants.Constraints.USER_PASSWORD_MIN_LENGHT)
                .WithError(RequestValidationErrors.CreateUserFailures.PasswordMinimumLenght)
            .MaximumLength(Constants.Constraints.USER_PASSWORD_MAX_LENGHT)
                .WithError(RequestValidationErrors.CreateUserFailures.PasswordMaximumLenght)
            .Must(x => x.Any(value => char.IsUpper(value)))
                .WithError(RequestValidationErrors.CreateUserFailures.PasswordFormatInvalidUpperCase)
            .Must(x => x.Any(value => char.IsLower(value)))
                .WithError(RequestValidationErrors.CreateUserFailures.PasswordFormatInvalidLowerCase)
            .Must(x => x.Any(value => char.IsDigit(value)))
                .WithError(RequestValidationErrors.CreateUserFailures.PasswordFormatInvalidNumber)
            .Matches("(?=.*[@#$%^&+=])")
                .WithError(RequestValidationErrors.CreateUserFailures.PasswordFormatInvalidNonAlphanumeric);

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
                .WithError(RequestValidationErrors.CreateUserFailures.PasswordIsRequired)
            .MinimumLength(Constants.Constraints.USER_PASSWORD_MIN_LENGHT)
                .WithError(RequestValidationErrors.CreateUserFailures.ConfirmPasswordMinimumLenght)
            .MaximumLength(Constants.Constraints.USER_PASSWORD_MAX_LENGHT)
                .WithError(RequestValidationErrors.CreateUserFailures.ConfirmPasswordMaximumLenght)
            .Must(x => x.Any(value => char.IsUpper(value)))
                .WithError(RequestValidationErrors.CreateUserFailures.ConfirmPasswordFormatInvalidUpperCase)
            .Must(x => x.Any(value => char.IsLower(value)))
                .WithError(RequestValidationErrors.CreateUserFailures.ConfirmPasswordFormatInvalidLowerCase)
            .Must(x => x.Any(value => char.IsDigit(value)))
                .WithError(RequestValidationErrors.CreateUserFailures.ConfirmPasswordFormatInvalidNumber)
            .Matches("(?=.*[@#$%^&+=])")
                .WithError(RequestValidationErrors.CreateUserFailures.ConfirmPasswordFormatInvalidNonAlphanumeric)
            .Equal(x => x.Password)
                .WithError(RequestValidationErrors.CreateUserFailures.PasswordsNotEquals);

        RuleFor(x => x.Name)
            .NotEmpty()
                .WithError(RequestValidationErrors.CreateUserFailures.NameIsRequired)
            .MaximumLength(Constants.Constraints.USER_FIRST_NAME_MAX_LENGHT)
                .WithError(RequestValidationErrors.CreateUserFailures.NameMaximumLenght);

        RuleFor(x => x.Contact)
            .MaximumLength(Constants.Constraints.USER_CONTACT_MAX_LENGHT)
                .WithError(RequestValidationErrors.CreateUserFailures.ContactMaximumLenght)
            .Matches(@"^\(\d{2}\)\d{5}-\d{4}$")
                .WithError(RequestValidationErrors.CreateUserFailures.ContactFormat)
                .When(x => x.Contact != string.Empty);
    }

}
