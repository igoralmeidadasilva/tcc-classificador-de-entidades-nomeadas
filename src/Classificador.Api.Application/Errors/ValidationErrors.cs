namespace Classificador.Api.Application.Errors;

public static class ValidationErrors
{
    public const string ERROR_MESSAGE = "Ocorrerão uma ou mais erros da validação.";
    public static class CreateUser
    {
        public static Error EmailIsRequired => new("CreateUser.Email.IsRequired", "O Email não pode ser vazio.");
        public static Error EmailMaximumLenght => new("CreateUser.Email.MaximumLenght", $"O Email não pode ser maior que {Constants.Constraints.USER_EMAIL_MAX_LENGHT} caracteres.");
        public static Error EmailFormat => new("CreateUser.Email.Format", "O Formato do email é inválido.");
        public static Error PasswordIsRequired => new("CreateUser.Password.IsRequired", "A senha não pode ser vazia.");
        public static Error PasswordMinimumLenght => new("CreateUser.Password.MinimumLenght", $"A senha não pode ser menor que {Constants.Constraints.USER_PASSWORD_MIN_LENGHT} caracteres.");
        public static Error PasswordMaximumLenght => new("CreateUser.Password.MaximumLenght", $"A senha não pode ser maior que {Constants.Constraints.USER_PASSWORD_MAX_LENGHT} caracteres.");
        public static Error PasswordFormatInvalidUpperCase => new("CreateUser.Password.FormatInvalid", "A senha deve conter ao menos uma letra maiúscula.");
        public static Error PasswordFormatInvalidLowerCase => new("CreateUser.Password.FormatInvalid", "A senha deve conter ao menos uma letra minúscula.");
        public static Error PasswordFormatInvalidNumber => new("CreateUser.Password.FormatInvalid", "A senha deve conter ao menos um número.");
        public static Error PasswordFormatInvalidNonAlphanumeric => new("CreateUser.Password.FormatInvalid", "A senha deve conter ao menos um caractere especial.");
        public static Error PasswordsNotEquals => new("CreateUser.Password.NotEquals", "As senhas fornecidas devem ser iguais.");
        public static Error NameIsRequired => new("CreateUser.Name.IsRequired", "O Nome não pode ser vazio.");
        public static Error NameMaximumLenght => new("CreateUser.Name.MaximumLenght", $"O Nome não pode ser maior que {Constants.Constraints.USER_FIRST_NAME_MAX_LENGHT} caracteres.");
        public static Error ContactMaximumLenght => new("CreateUser.Contact.MaximumLenght", $"O contato não pode ser maior que {Constants.Constraints.USER_CONTACT_MAX_LENGHT} caracteres.");
        public static Error ContactFormat => new("CreateUser.Contact.Format", "O número de contato deve serguir o formato (XX)XXXXX-XXXX");
    }

    public static class UpdateUserRole
    {
        public static Error UserIdIsRequired => new("UpdateUserRole.Id.IsRequired", "O Id do usuário não pode ser vazio.");
    }


}
