namespace Classificador.Api.Application.Commands.LoginUser;

public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
                .WithError(CommandErrors.LoginUserFailures.EmailIsRequired)
            .MaximumLength(Constants.Constraints.USER_EMAIL_MAX_LENGHT)
                .WithError(CommandErrors.LoginUserFailures.EmailMaximumLenght)
            .EmailAddress()
                .WithError(CommandErrors.LoginUserFailures.EmailFormat);

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithError(CommandErrors.LoginUserFailures.PasswordIsRequired)
            .MinimumLength(Constants.Constraints.USER_PASSWORD_MIN_LENGHT)
                .WithError(CommandErrors.LoginUserFailures.PasswordMinimumLenght)
            .MaximumLength(Constants.Constraints.USER_PASSWORD_MAX_LENGHT)
                .WithError(CommandErrors.LoginUserFailures.PasswordMaximumLenght)
            .Must(x => x.Any(value => char.IsUpper(value)))
                .WithError(CommandErrors.LoginUserFailures.PasswordFormatInvalidUpperCase)
            .Must(x => x.Any(value => char.IsLower(value)))
                .WithError(CommandErrors.LoginUserFailures.PasswordFormatInvalidLowerCase)
            .Must(x => x.Any(value => char.IsDigit(value)))
                .WithError(CommandErrors.LoginUserFailures.PasswordFormatInvalidNumber)
            .Matches("(?=.*[@#$%^&+=])")
                .WithError(CommandErrors.LoginUserFailures.PasswordFormatInvalidNonAlphanumeric);
    }
}
