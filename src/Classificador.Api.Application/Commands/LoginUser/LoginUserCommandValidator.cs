namespace Classificador.Api.Application.Commands.LoginUser;

public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
                .WithError(ValidationErrors.LoginUser.EmailIsRequired)
            .MaximumLength(Constants.Constraints.USER_EMAIL_MAX_LENGHT)
                .WithError(ValidationErrors.LoginUser.EmailIsRequired)
            .EmailAddress()
                .WithError(ValidationErrors.LoginUser.EmailFormat);
        
        RuleFor(x => x.Password)
            .NotEmpty()
                .WithError(ValidationErrors.LoginUser.PasswordIsRequired)
            .MinimumLength(Constants.Constraints.USER_PASSWORD_MIN_LENGHT)
                .WithError(ValidationErrors.LoginUser.PasswordMinimumLenght)
            .MaximumLength(Constants.Constraints.USER_PASSWORD_MAX_LENGHT)
                .WithError(ValidationErrors.LoginUser.PasswordMaximumLenght)
            .Must(x => x.Any(value => char.IsUpper(value)))
                .WithError(ValidationErrors.LoginUser.PasswordFormatInvalidUpperCase)
            .Must(x => x.Any(value => char.IsLower(value)))
                .WithError(ValidationErrors.LoginUser.PasswordFormatInvalidLowerCase)
            .Must(x => x.Any(value => char.IsDigit(value)))
                .WithError(ValidationErrors.LoginUser.PasswordFormatInvalidNumber)
            .Matches("(?=.*[@#$%^&+=])")
                .WithError(ValidationErrors.LoginUser.PasswordFormatInvalidNonAlphanumeric);

    }
}
