namespace Classificador.Api.Application.Errors;

public static class CommandErrors
{
    public static string ValidationErrorCore(string type) => $"Validation.{type}";
    public static string ValidationErrorMessage() => "Um ou mais erros de validação foram encontrados. Por favor, revise os seus dados.";

    public static class CreateUserFailures
    {
        public static PropertyFailure EmailIsRequired => new("CreateUser.Email.IsRequired", "O Email não pode ser vazio.");
        public static PropertyFailure EmailMaximumLenght 
            => new("CreateUser.Email.MaximumLenght", $"O Email não pode ser maior que {Constants.Constraints.USER_EMAIL_MAX_LENGHT} caracteres.");
        public static PropertyFailure EmailFormat => new("CreateUser.Email.FormatInvalid", "O Formato do email é inválido.");
        public static PropertyFailure PasswordIsRequired => new("CreateUser.Password.IsRequired", "A senha não pode ser vazia.");
        public static PropertyFailure PasswordMinimumLenght => 
            new("CreateUser.Password.MinimumLenght", $"A senha não pode ser menor que {Constants.Constraints.USER_PASSWORD_MIN_LENGHT} caracteres.");
        public static PropertyFailure PasswordMaximumLenght => 
            new("CreateUser.Password.MaximumLenght", $"A senha não pode ser maior que {Constants.Constraints.USER_PASSWORD_MAX_LENGHT} caracteres.");
        public static PropertyFailure PasswordFormatInvalidUpperCase => new("CreateUser.Password.RequiredUpperCase", "A senha deve conter ao menos uma letra maiúscula.");
        public static PropertyFailure PasswordFormatInvalidLowerCase => new("CreateUser.Password.RequiredLowerCase", "A senha deve conter ao menos uma letra minúscula.");
        public static PropertyFailure PasswordFormatInvalidNumber => new("CreateUser.Password.RequiredNumber", "A senha deve conter ao menos um número.");
        public static PropertyFailure PasswordFormatInvalidNonAlphanumeric 
            => new("CreateUser.Password.RequiredAlphanumeric", "A senha deve conter ao menos um caractere especial.");
        public static PropertyFailure ConfirmPasswordIsRequired => new("CreateUser.ConfirmPassword.IsRequired", "A confirmação da senha não pode ser vazia.");
        public static PropertyFailure ConfirmPasswordMinimumLenght => 
            new("CreateUser.ConfirmPassword.MinimumLenght", $"A confirmação da senha não pode ser menor que {Constants.Constraints.USER_PASSWORD_MIN_LENGHT} caracteres.");
        public static PropertyFailure ConfirmPasswordMaximumLenght => 
            new("CreateUser.ConfirmPassword.MaximumLenght", $"A confirmação da senha não pode ser maior que {Constants.Constraints.USER_PASSWORD_MAX_LENGHT} caracteres.");
        public static PropertyFailure ConfirmPasswordFormatInvalidUpperCase => 
            new("CreateUser.ConfirmPassword.RequiredUpperCase", "A confirmação da senha deve conter ao menos uma letra maiúscula.");
        public static PropertyFailure ConfirmPasswordFormatInvalidLowerCase => 
            new("CreateUser.ConfirmPassword.RequiredLowerCase", "A confirmação da senha deve conter ao menos uma letra minúscula.");
        public static PropertyFailure ConfirmPasswordFormatInvalidNumber => 
            new("CreateUser.ConfirmPassword.RequiredNumber", "A confirmação dasenha deve conter ao menos um número.");
        public static PropertyFailure ConfirmPasswordFormatInvalidNonAlphanumeric => 
            new("CreateUser.ConfirmPassword.RequiredAlphanumeric", "A confirmação da senha deve conter ao menos um caractere especial.");
        public static PropertyFailure PasswordsNotEquals => new("CreateUser.Password.NotEquals", "As senhas fornecidas devem ser iguais.");
        public static PropertyFailure NameIsRequired => new("CreateUser.Name.IsRequired", "O Nome não pode ser vazio.");
        public static PropertyFailure NameMaximumLenght => 
            new("CreateUser.Name.MaximumLenght", $"O Nome não pode ser maior que {Constants.Constraints.USER_FIRST_NAME_MAX_LENGHT} caracteres.");
        public static PropertyFailure ContactMaximumLenght => 
            new("CreateUser.Contact.MaximumLenght", $"O contato não pode ser maior que {Constants.Constraints.USER_CONTACT_MAX_LENGHT} caracteres.");
        public static PropertyFailure ContactFormat => new("CreateUser.Contact.FormatInvalid", "O número de contato deve serguir o formato (XX)XXXXX-XXXX");
    }
    
    public static class UpdateUserRoleFailures
    {
        public static PropertyFailure UserIdIsRequired => new("UpdateUserRole.Id.IsRequired", "O Id do usuário não pode ser vazio.");
    }
    
    public static class LoginUserFailures
    {
        public static PropertyFailure EmailIsRequired => new("LoginUser.Email.IsRequired", "O Email de login não pode ser vazio.");
        public static PropertyFailure EmailMaximumLenght => 
            new("LoginUser.Email.MaximumLenght", $"O Email não pode ser maior que {Constants.Constraints.USER_EMAIL_MAX_LENGHT} caracteres.");
        public static PropertyFailure EmailFormat => new("LoginUser.Email.FormatInvalid", "O Formato do email é inválido.");
        public static PropertyFailure PasswordIsRequired => new("LoginUser.Password.IsRequired", "A senha não pode ser vazia.");
        public static PropertyFailure PasswordMinimumLenght => 
            new("LoginUser.Password.MinimumLenght", $"A senha não pode ser menor que {Constants.Constraints.USER_PASSWORD_MIN_LENGHT} caracteres.");
        public static PropertyFailure PasswordMaximumLenght => 
            new("LoginUser.Password.MaximumLenght", $"A senha não pode ser maior que {Constants.Constraints.USER_PASSWORD_MAX_LENGHT} caracteres.");
        public static PropertyFailure PasswordFormatInvalidUpperCase => new("LoginUser.Password.RequiredUpperCase", "A senha deve conter ao menos uma letra maiúscula.");
        public static PropertyFailure PasswordFormatInvalidLowerCase => new("LoginUser.Password.RequireLowerCase", "A senha deve conter ao menos uma letra minúscula.");
        public static PropertyFailure PasswordFormatInvalidNumber => new("LoginUser.Password.RequiredNumber", "A senha deve conter ao menos um número.");
        public static PropertyFailure PasswordFormatInvalidNonAlphanumeric => 
            new("LoginUser.Password.RequiredAlphanumeric", "A senha deve conter ao menos um caractere especial.");
    }
    
    public static class CreatePrescribingInformationTxtFailures
    {
        public static PropertyFailure NameIsRequired => new("CreatePrescribingInformationTxt.Name.IsRequired", "O Nome não pode ser vazio.");
        public static PropertyFailure NameMaximumLenght => 
            new("CreatePrescribingInformationTxt.Name.MaximumLenght", $"O Nome não pode ser maior que {Constants.Constraints.PRESCRIBING_INFORMATION_NAME_MAX_LENGHT} caracteres.");
        public static PropertyFailure FileIsRequired => new("CreatePrescribingInformationTxt.File.IsRequired", "O arquivo não pode ser vazio.");
        public static PropertyFailure FileExtension => new("CreatePrescribingInformationTxt.File.Extension", "A extensão do arquivo deve ser '.txt'.");
    }
}
