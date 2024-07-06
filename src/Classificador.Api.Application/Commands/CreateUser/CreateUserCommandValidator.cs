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
            .Must(RequireUppercase).WithError(ValidationErrors.CreateUser.PasswordFormatInvalidUpperCase)
            .Must(RequiredLowerCase).WithError(ValidationErrors.CreateUser.PasswordFormatInvalidLowerCase)
            .Must(RequireDigit).WithError(ValidationErrors.CreateUser.PasswordFormatInvalidNumber)
            .Must(RequireNonAlphanumeric).WithError(ValidationErrors.CreateUser.PasswordFormatInvalidNonAlphanumeric);

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithError(ValidationErrors.CreateUser.PasswordIsRequired)
            .MinimumLength(Constants.Constraints.USER_PASSWORD_MIN_LENGHT).WithError(ValidationErrors.CreateUser.PasswordMinimumLenght)
            .MaximumLength(Constants.Constraints.USER_PASSWORD_MAX_LENGHT).WithError(ValidationErrors.CreateUser.PasswordMaximumLenght)
            .Must(RequireUppercase).WithError(ValidationErrors.CreateUser.PasswordFormatInvalidUpperCase)
            .Must(RequiredLowerCase).WithError(ValidationErrors.CreateUser.PasswordFormatInvalidLowerCase)
            .Must(RequireDigit).WithError(ValidationErrors.CreateUser.PasswordFormatInvalidNumber)
            .Must(RequireNonAlphanumeric).WithError(ValidationErrors.CreateUser.PasswordFormatInvalidNonAlphanumeric)
            // TODO: Essa verificação não está fazendo uso do serviço de HashPasswrods
            .Equal(x => x.Password).WithError(ValidationErrors.CreateUser.PasswordsNotEquals);

        RuleFor(x => x.Name)
            .NotEmpty().WithError(ValidationErrors.CreateUser.NameIsRequired)
            .MaximumLength(Constants.Constraints.USER_FIRST_NAME_MAX_LENGHT).WithError(ValidationErrors.CreateUser.NameMaximumLenght);

        RuleFor(x => x.Contact)
            .MaximumLength(Constants.Constraints.USER_CONTACT_MAX_LENGHT).WithError(ValidationErrors.CreateUser.ContactMaximumLenght)
            .Matches(@"^\(\d{2}\)\d{5}-\d{4}$").WithError(ValidationErrors.CreateUser.ContactFormat);
    }

    private bool RequireDigit(string value)
    {
        if (value.Any(x => char.IsDigit(x)))
        {
            return true;
        }
        return false;
    }

    private bool RequiredLowerCase(string value)
    {
        if (value.Any(x => char.IsLower(x)))
        {
            return true;
        }
        return false;
    }

    private bool RequireUppercase(string value)
    {
        if (value.Any(x => char.IsUpper(x)))
        {
            return true;
        }
        return false;
    }

    private bool RequireNonAlphanumeric(string value)
    {
        if (Regex.IsMatch(value, "(?=.*[@#$%^&+=])"))
        {
            return true;
        }
        return false;
    }
}
