namespace Classificador.Api.Application.Commands.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithError(ValidationErrors.CreateUser.EmailIsRequired)
            .MaximumLength(Constants.Constraints.USER_EMAIL_MAX_LENGHT).WithError(ValidationErrors.CreateUser.EmailMaximumLenght)
            .EmailAddress().WithError(ValidationErrors.CreateUser.EmailFormat);

        RuleFor(x => x.Password)
            .NotEmpty().WithError(ValidationErrors.CreateUser.PasswordIsRequired)
            .MinimumLength(Constants.Constraints.USER_PASSWORD_MIN_LENGHT).WithError(ValidationErrors.CreateUser.PasswordMinimumLenght)
            .MaximumLength(Constants.Constraints.USER_PASSWORD_MAX_LENGHT).WithError(ValidationErrors.CreateUser.PasswordMaximumLenght)
            .Must(x => x.Any(value => char.IsUpper(value))).WithError(ValidationErrors.CreateUser.PasswordFormatInvalidUpperCase)
            .Must(x => x.Any(value => char.IsLower(value))).WithError(ValidationErrors.CreateUser.PasswordFormatInvalidLowerCase)
            .Must(x => x.Any(value => char.IsDigit(value))).WithError(ValidationErrors.CreateUser.PasswordFormatInvalidNumber)
            .Matches("(?=.*[@#$%^&+=])").WithError(ValidationErrors.CreateUser.PasswordFormatInvalidNonAlphanumeric);

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithError(ValidationErrors.CreateUser.PasswordIsRequired)
            .MinimumLength(Constants.Constraints.USER_PASSWORD_MIN_LENGHT).WithError(ValidationErrors.CreateUser.ConfirmPasswordMinimumLenght)
            .MaximumLength(Constants.Constraints.USER_PASSWORD_MAX_LENGHT).WithError(ValidationErrors.CreateUser.ConfirmPasswordMaximumLenght)
            .Must(x => x.Any(value => char.IsUpper(value))).WithError(ValidationErrors.CreateUser.ConfirmPasswordFormatInvalidUpperCase)
            .Must(x => x.Any(value => char.IsLower(value))).WithError(ValidationErrors.CreateUser.ConfirmPasswordFormatInvalidLowerCase)
            .Must(x => x.Any(value => char.IsDigit(value))).WithError(ValidationErrors.CreateUser.ConfirmPasswordFormatInvalidNumber)
            .Matches("(?=.*[@#$%^&+=])").WithError(ValidationErrors.CreateUser.ConfirmPasswordFormatInvalidNonAlphanumeric)
            .Equal(x => x.Password).WithError(ValidationErrors.CreateUser.PasswordsNotEquals);

        RuleFor(x => x.Name)
            .NotEmpty().WithError(ValidationErrors.CreateUser.NameIsRequired)
            .MaximumLength(Constants.Constraints.USER_FIRST_NAME_MAX_LENGHT).WithError(ValidationErrors.CreateUser.NameMaximumLenght);

        RuleFor(x => x.Contact)
            .MaximumLength(Constants.Constraints.USER_CONTACT_MAX_LENGHT).WithError(ValidationErrors.CreateUser.ContactMaximumLenght)
            .Matches(@"^\(\d{2}\)\d{5}-\d{4}$").WithError(ValidationErrors.CreateUser.ContactFormat)
            .When(x => x.Contact != string.Empty);
    }

}
