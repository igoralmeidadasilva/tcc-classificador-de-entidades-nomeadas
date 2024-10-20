using Classificador.Api.SharedKernel.Shared.Results;

namespace Classificador.Api.Domain.Core.Errors;

public static class DomainErrors
{
    public static class User
    {
        private const string ENTITY = nameof(Entities.User); 
        public static Error UserNotFound => Error.Create("User.NotFound", "Não foi possivel encontrar o usuário.", ErrorType.NotFound);
        public static Error EmailNotExists =>
            Error.Create
            (
                "User.Email.NotExists", 
                "Não conseguimos encontrar o seu e-mai. Por favor, verifique se o endereço de e-mail fornecido está correto e tente novamente", 
                ErrorType.NotFound
            );
        public static Error EmailAlreadyExists => Error.Create("User.Email.AlreadyExists", "O Email já existe.", ErrorType.Conflict);
        public static Error AuthenticationPasswordFailed => Error.Create("User.AuthenticationPassword.Failed", "Credênciais Inválidas.", ErrorType.Unauthorized);
    }

    public static class NamedEntity
    {
        public static Error NamedEntityNotFound => Error.Create("NamedEntity.NotFound", "Não foi possivel encontrar a entidade nomeada.", ErrorType.NotFound);
        public static Error NamedEntityNoneWereFound => 
            Error.Create("NamedEntity.NoneWereFound", "Não foi possivel encontrar nenhuma entidade nomeada.", ErrorType.NotFound);
    }

    public static class Category
    {
        public static Error CategoryEntityNoneWereFound => 
            Error.Create("Category.NoneWereFound", "Não foi possivel encontrar nenhuma categoria.", ErrorType.NotFound);
        public static Error CategoryEntityNotFound => Error.Create("Category.NotFound", "Não foi possivel encontrar a categoria.", ErrorType.NotFound);
        public static Error NameAlredyExists => Error.Create("Category.Name.AlreadyExists", "Uma categoria com esse nome já existe.", ErrorType.Conflict);
    }

    public static class Specialty
    {
        public static Error SpecialtyEntityNoneWereFound => 
            Error.Create("Specialty.NoneWereFound", "Não foi possivel encontrar nenhuma especialidade.", ErrorType.NotFound);
        public static Error SpecialtyEntityNotFound => Error.Create("Specialty.NotFound", "Não foi possivel encontrar a especialidade.", ErrorType.NotFound);
        public static Error NameAlredyExists => Error.Create("Specialty.Name.AlreadyExists", "Uma especialidae com esse nome já existe.", ErrorType.Conflict);
    }

    public static class PrescribingInformation
    {
        public static Error PrescribingInformationEntityNoneWereFound  => 
            Error.Create("PrescribingInformation.NotFound", "Não foi possivel encontrar nenhuma bula farmacêutica.", ErrorType.NotFound);
    }

    public static class Classification
    {
        public static Error ClassificationsPendingNotFound =>
            Error.Create("Classification.PendingNotFound", "Não foi possivel encontrar nenhuma classificação pendente.", ErrorType.NotFound);

        public static Error ClassificationsCompletedNotFound =>
            Error.Create("Classification.PendingNotFound", "Não foi possivel encontrar nenhuma classificação completa.", ErrorType.NotFound);

        public static Error ClassificationAlreadyExists =>
            Error.Create("Classification.AlreadyExists", "A Classificação já existe.", ErrorType.NotFound);
    }

    public static class EmailSend
    {
        public static Error FailedToSendEmail => Error.Create("EmailSend.Failure", "Não foi possivel enviar o email.", ErrorType.Failure);
    }
}
