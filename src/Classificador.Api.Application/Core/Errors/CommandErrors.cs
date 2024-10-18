using Classificador.Api.Domain;

namespace Classificador.Api.Application.Core.Errors;

public static class CommandErrors
{
    public static class CreateUserFailures
    {
        public static Error EmailIsRequired => Error.Create("CreateUser.Email.IsRequired", "O Email não pode ser vazio.", ErrorType.Validation);
        public static Error EmailMaximumLenght => Error.Create(
            "CreateUser.Email.MaximumLenght", 
            $"O Email não pode ser maior que {Constants.Constraints.User.EMAIL_MAX_LENGHT} caracteres.", 
            ErrorType.Validation);
        public static Error EmailFormat => Error.Create("CreateUser.Email.FormatInvalid", "O Formato do email é inválido.", ErrorType.Validation);
        public static Error PasswordIsRequired => Error.Create("CreateUser.Password.IsRequired", "A senha não pode ser vazia.", ErrorType.Validation);
        public static Error PasswordMinimumLenght => Error.Create(
            "CreateUser.Password.MinimumLenght", 
            $"A senha não pode ser menor que {Constants.Constraints.User.PASSWORD_MIN_LENGHT} caracteres.", 
            ErrorType.Validation);
        public static Error PasswordMaximumLenght => Error.Create(
            "CreateUser.Password.MaximumLenght", 
            $"A senha não pode ser maior que {Constants.Constraints.User.PASSWORD_MAX_LENGHT} caracteres.", 
            ErrorType.Validation);
        public static Error PasswordFormatInvalidUpperCase => Error.Create(
            "CreateUser.Password.RequiredUpperCase", 
            "A senha deve conter ao menos uma letra maiúscula.", 
            ErrorType.Validation);
        public static Error PasswordFormatInvalidLowerCase => Error.Create("CreateUser.Password.RequiredLowerCase", "A senha deve conter ao menos uma letra minúscula.");
        public static Error PasswordFormatInvalidNumber => Error.Create("CreateUser.Password.RequiredNumber", "A senha deve conter ao menos um número.");
        public static Error PasswordFormatInvalidNonAlphanumeric
            => Error.Create("CreateUser.Password.RequiredAlphanumeric", "A senha deve conter ao menos um caractere especial.");
        public static Error ConfirmPasswordIsRequired => Error.Create("CreateUser.ConfirmPassword.IsRequired", "A confirmação da senha não pode ser vazia.");
        public static Error ConfirmPasswordMinimumLenght => Error.Create(
                "CreateUser.ConfirmPassword.MinimumLenght", 
                $"A confirmação da senha não pode ser menor que {Constants.Constraints.User.PASSWORD_MIN_LENGHT} caracteres.");
        public static Error ConfirmPasswordMaximumLenght => Error.Create(
                "CreateUser.ConfirmPassword.MaximumLenght", 
                $"A confirmação da senha não pode ser maior que {Constants.Constraints.User.PASSWORD_MAX_LENGHT} caracteres.");
        public static Error ConfirmPasswordFormatInvalidUpperCase =>
            Error.Create("CreateUser.ConfirmPassword.RequiredUpperCase", "A confirmação da senha deve conter ao menos uma letra maiúscula.", ErrorType.Validation);
        public static Error ConfirmPasswordFormatInvalidLowerCase =>
            Error.Create("CreateUser.ConfirmPassword.RequiredLowerCase", "A confirmação da senha deve conter ao menos uma letra minúscula.", ErrorType.Validation);
        public static Error ConfirmPasswordFormatInvalidNumber =>
            Error.Create("CreateUser.ConfirmPassword.RequiredNumber", "A confirmação da senha deve conter ao menos um número.", ErrorType.Validation);
        public static Error ConfirmPasswordFormatInvalidNonAlphanumeric => Error.Create(
            "CreateUser.ConfirmPassword.RequiredAlphanumeric", 
            "A confirmação da senha deve conter ao menos um caractere especial.", 
            ErrorType.Validation);
        public static Error PasswordsNotEquals => Error.Create("CreateUser.Password.NotEquals", "As senhas fornecidas devem ser iguais.", ErrorType.Validation);
        public static Error NameIsRequired => Error.Create("CreateUser.Name.IsRequired", "O Nome não pode ser vazio.", ErrorType.Validation);
        public static Error NameMaximumLenght => Error.Create(
            "CreateUser.Name.MaximumLenght", 
            $"O Nome não pode ser maior que {Constants.Constraints.User.NAME_MAX_LENGHT} caracteres.", 
            ErrorType.Validation);
        public static Error ContactMaximumLenght => Error.Create(
            "CreateUser.Contact.MaximumLenght", 
            $"O contato não pode ser maior que {Constants.Constraints.User.CONTACT_MAX_LENGHT} caracteres.", 
            ErrorType.Validation);
        public static Error ContactFormat => Error.Create(
            "CreateUser.Contact.FormatInvalid", 
            "O número de contato deve serguir o formato (XX)XXXXX-XXXX", 
            ErrorType.Validation);
        public static Error SpecialtyIsRequired => Error.Create("CreateUser.Specialty.IsRequired", "A Especialidade não pode ser vazia.", ErrorType.Validation);
    }

    public static class UpdateUserRoleFailures
    {
        public static Error UserIdIsRequired => Error.Create("UpdateUserRole.Id.IsRequired", "O Id do usuário não pode ser vazio.", ErrorType.Validation);
    }

    public static class LoginUserFailures
    {
        public static Error EmailIsRequired => Error.Create("LoginUser.Email.IsRequired", "O Email de login não pode ser vazio.", ErrorType.Validation);
        public static Error EmailMaximumLenght => Error.Create(
            "LoginUser.Email.MaximumLenght", 
            $"O Email não pode ser maior que {Constants.Constraints.User.EMAIL_MAX_LENGHT} caracteres.", 
            ErrorType.Validation);
        public static Error EmailFormat => Error.Create("LoginUser.Email.FormatInvalid", "O Formato do email é inválido.", ErrorType.Validation);
        public static Error EmailNotFound => Error.Create("LoginUser.Email.NotFound", "O Email não existe ou está incorreto.", ErrorType.Validation);
        public static Error PasswordIsRequired => Error.Create("LoginUser.Password.IsRequired", "A senha não pode ser vazia.", ErrorType.Validation);
        public static Error PasswordMinimumLenght => Error.Create(
            "LoginUser.Password.MinimumLenght", 
            $"A senha não pode ser menor que {Constants.Constraints.User.PASSWORD_MIN_LENGHT} caracteres.", 
            ErrorType.Validation);
        public static Error PasswordMaximumLenght => Error.Create(
            "LoginUser.Password.MaximumLenght", 
            $"A senha não pode ser maior que {Constants.Constraints.User.PASSWORD_MAX_LENGHT} caracteres.", 
            ErrorType.Validation);
        public static Error PasswordFormatInvalidUpperCase => Error.Create(
            "LoginUser.Password.RequiredUpperCase", 
            "A senha deve conter ao menos uma letra maiúscula.", 
            ErrorType.Validation);
        public static Error PasswordFormatInvalidLowerCase => Error.Create(
            "LoginUser.Password.RequireLowerCase", 
            "A senha deve conter ao menos uma letra minúscula.",
            ErrorType.Validation);
        public static Error PasswordFormatInvalidNumber => Error.Create(
            "LoginUser.Password.RequiredNumber", 
            "A senha deve conter ao menos um número.", ErrorType.Validation);
        public static Error PasswordFormatInvalidNonAlphanumeric =>
            Error.Create("LoginUser.Password.RequiredAlphanumeric", "A senha deve conter ao menos um caractere especial.", ErrorType.Validation);
    }

    public static class CreatePrescribingInformationTxtFailures
    {
        public static Error NameIsRequired => Error.Create("CreatePrescribingInformationTxt.Name.IsRequired", "O Nome não pode ser vazio.", ErrorType.Validation);
        public static Error NameMaximumLenght => Error.Create(
            "CreatePrescribingInformationTxt.Name.MaximumLenght", 
            $"O Nome não pode ser maior que {Constants.Constraints.PrescribingInformation.NAME_MAX_LENGHT} caracteres.", 
            ErrorType.Validation);
        public static Error FileIsRequired => Error.Create(
            "CreatePrescribingInformationTxt.File.IsRequired", 
            "O arquivo não pode ser vazio.", 
            ErrorType.Validation);
        public static Error FileExtension => Error.Create(
            "CreatePrescribingInformationTxt.File.Extension", 
            "A extensão do arquivo deve ser '.txt'.",
            ErrorType.Validation);
    }

    public static class CreateCategoryFailures
    {
        public static Error NameIsRequired => Error.Create("CreateCategory.Name.IsRequired", "O Nome não pode ser vazio.", ErrorType.Validation);
        public static Error NameMaximumLenght => Error.Create(
            "CreateCategory.Name.MaximumLenght", 
            $"O Nome não pode ser maior que {Constants.Constraints.Category.NAME_MAX_LENGHT} caracteres.", 
            ErrorType.Validation);
    }

    public static class CreateSpecialtyFailures
    {
        public static Error NameIsRequired => Error.Create("CreateSpecialty.Name.IsRequired", "O Nome não pode ser vazio.", ErrorType.Validation);
        public static Error NameMaximumLenght => Error.Create(
            "CreateSpecialty.Name.MaximumLenght", 
            $"O Nome não pode ser maior que {Constants.Constraints.Specialty.NAME_MAX_LENGHT } caracteres.", 
            ErrorType.Validation);
    }

    public static class SendEmailToContact
    {
        public static Error NameIsRequired => Error.Create("SendEmailToContact.Name.IsRequired", "O Nome não pode ser vazio.", ErrorType.Validation);
        public static Error SubjectIsRequired => Error.Create("SendEmailToContact.Subject.IsRequired", "O Assunto não pode ser vazio.", ErrorType.Validation);
        public static Error EmailIsRequired => Error.Create("SendEmailToContact.Email.IsRequired", "O Endereço de Email não pode ser vazio.", ErrorType.Validation);
        public static Error EmailFormat => Error.Create("SendEmailToContact.Email.FormatInvalid", "O Formato do email é inválido.", ErrorType.Validation);
        public static Error MessageIsRequired => Error.Create("SendEmailToContact.Message.IsRequired", "A Mensagem não pode ser vazia.", ErrorType.Validation);

    }

    public static class CreateClassificationFailures
    {
        public static Error IdUserIsRequired => Error.Create("CreateClassification.IdUser.IsRequired", "O usuário não pode ser vazio.", ErrorType.Validation);
        public static Error IdNamedEntityIsRequired => 
            Error.Create("CreateClassification.IdNamedEntity.IsRequired", "A entidade não pode ser vazia.", ErrorType.Validation);
        public static Error IdCategoryIsRequired => 
            Error.Create("CreateClassification.IdCategory.IsRequired", "A categoria não pode ser vazia.", ErrorType.Validation);
    }

    public static class UpdateClassificationToCompletedFailures
    {
        public static Error UserIdIsRequired => 
            Error.Create("UpdateClassificationToCompleted.IdUser.IsRequired", "O Id do usuário não pode ser vazio.", ErrorType.Validation);
        public static Error PrescribingInformationIdIsRequired =>
            Error.Create("UpdateClassificationToCompleted.IdPrescribingInformation.IsRequired", "O Id da bula não pode ser vazia.", ErrorType.Validation);
    }

    public static class DeletePendingClassificationFailures
    {
        public static Error ClassificationIdIsRequired =>
            Error.Create("DeletePendingClassification.IdClassification.IsRequired", "O Id da classificação não pode ser vazia.", ErrorType.Validation);
    }
}
