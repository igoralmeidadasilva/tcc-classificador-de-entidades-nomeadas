namespace Classificador.Api.Application.Commands.LoginUser;

public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
                .WithError(RequestValidationErrors.LoginUserFailures.EmailIsRequired)
            .MaximumLength(Constants.Constraints.USER_EMAIL_MAX_LENGHT)
                .WithError(RequestValidationErrors.LoginUserFailures.EmailIsRequired)
            .EmailAddress()
                .WithError(RequestValidationErrors.LoginUserFailures.EmailFormat);
        
        RuleFor(x => x.Password)
            .NotEmpty()
                .WithError(RequestValidationErrors.LoginUserFailures.PasswordIsRequired)
            .MinimumLength(Constants.Constraints.USER_PASSWORD_MIN_LENGHT)
                .WithError(RequestValidationErrors.LoginUserFailures.PasswordMinimumLenght)
            .MaximumLength(Constants.Constraints.USER_PASSWORD_MAX_LENGHT)
                .WithError(RequestValidationErrors.LoginUserFailures.PasswordMaximumLenght)
            .Must(x => x.Any(value => char.IsUpper(value)))
                .WithError(RequestValidationErrors.LoginUserFailures.PasswordFormatInvalidUpperCase)
            .Must(x => x.Any(value => char.IsLower(value)))
                .WithError(RequestValidationErrors.LoginUserFailures.PasswordFormatInvalidLowerCase)
            .Must(x => x.Any(value => char.IsDigit(value)))
                .WithError(RequestValidationErrors.LoginUserFailures.PasswordFormatInvalidNumber)
            .Matches("(?=.*[@#$%^&+=])")
                .WithError(RequestValidationErrors.LoginUserFailures.PasswordFormatInvalidNonAlphanumeric);

    }
}
