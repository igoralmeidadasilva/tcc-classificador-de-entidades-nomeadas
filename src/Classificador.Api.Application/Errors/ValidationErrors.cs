namespace Classificador.Api.Application.Errors;

internal static class ValidationErrors
{
    internal const string ERROR_MESSAGE = "Ocorrerão uma ou mais erros da validação.";
    internal static class CreateUser
    {
        internal static Error EmailIsRequired => new("CreateUser.Email.IsRequired", "O Email não pode ser vazio.");
        internal static Error EmailMaximumLenght => new("CreateUser.Email.MaximumLenght", $"O Email não pode ser maior que {Constants.Constraints.USER_EMAIL_MAX_LENGHT} caracteres.");
        internal static Error EmailFormat => new("CreateUser.Email.Format", "O Formato do email é inválido.");
        internal static Error PasswordIsRequired => new("CreateUser.Password.IsRequired", "A senha não pode ser vazia.");
        internal static Error PasswordMinimumLenght => new("CreateUser.Password.MinimumLenght", $"A senha não pode ser menor que {Constants.Constraints.USER_PASSWORD_MIN_LENGHT} caracteres.");
        internal static Error PasswordMaximumLenght => new("CreateUser.Password.MaximumLenght", $"A senha não pode ser maior que {Constants.Constraints.USER_PASSWORD_MAX_LENGHT} caracteres.");
        internal static Error PasswordFormatInvalidUpperCase => new("CreateUser.Password.FormatInvalid", "A senha deve conter ao menos uma letra maiúscula.");
        internal static Error PasswordFormatInvalidLowerCase => new("CreateUser.Password.FormatInvalid", "A senha deve conter ao menos uma letra minúscula.");
        internal static Error PasswordFormatInvalidNumber => new("CreateUser.Password.FormatInvalid", "A senha deve conter ao menos um número.");
        internal static Error PasswordFormatInvalidNonAlphanumeric => new("CreateUser.Password.FormatInvalid", "A senha deve conter ao menos um caractere especial.");
        internal static Error NameIsRequired => new("CreateUser.Name.IsRequired", "O Nome não pode ser vazio.");
        internal static Error NameMaximumLenght => new("CreateUser.Name.MaximumLenght", $"O Nome não pode ser maior que {Constants.Constraints.USER_FIRST_NAME_MAX_LENGHT} caracteres.");
        internal static Error ContactMaximumLenght => new("CreateUser.Contact.MaximumLenght", $"O contato não pode ser maior que {Constants.Constraints.USER_CONTACT_MAX_LENGHT} caracteres.");
        internal static Error ContactFormat => new("CreateUser.Contact.Format", $"O número de contato deve serguir o formato (XX)XXXXX-XXXX");
    }
}
