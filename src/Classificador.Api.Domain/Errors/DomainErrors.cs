namespace Classificador.Api.Domain.Errors;

public static class DomainErrors
{
    public static class General
    {
        public static Error NotFound => new("General.NotFound", "Não foi possivel encontrar o recurso que você esta tentando acessar.", ErrorType.NotFound);
    }
    
    public static class User
    {
        public static Error UserNotFound => new("User.NotFound", "Não foi possivel encontrar o usuário.", ErrorType.NotFound);
        public static Error EmailNotExists => 
            new("User.Email.NotExists", "Não conseguimos encontrar o seu e-mai. Por favor, verifique se o endereço de e-mail fornecido está correto e tente novamente", ErrorType.NotFound);
        public static Error EmailAlreadyExists => new("User.Email.AlreadyExists", "O Email já existe.", ErrorType.Conflict);
        public static Error AuthenticationPasswordFailed => new("User.AuthenticationPassword.Failed", "Credênciais Inválidas.", ErrorType.Unauthorized);
    }

    public static class NamedEntity
    {
        public static Error NamedEntityNotFound => new("NamedEntity.NotFound", "Não foi possivel encontrar a entidade nomeada.", ErrorType.NotFound);
        public static Error NamedEntityNoneWereFound => new("NamedEntity.NoneWereFound", "Não foi possivel encontrar nenhuma entidade nomeada.", ErrorType.NotFound);
    }

    public static class Category
    {
        public static Error CategoryEntityNoneWereFound => new("Category.NoneWereFound", "Não foi possivel encontrar nenhuma categoria.", ErrorType.NotFound);
        public static Error CategoryEntityNotFound => new("Category.NotFound", "Não foi possivel encontrar a categoria.", ErrorType.NotFound);
        public static Error NameAlredyExists => new("Category.Name.AlreadyExists", "Uma categoria com esse nome já existe.", ErrorType.Conflict);
    }

    public static class Specialty
    {
        public static Error SpecialtyEntityNotFound => new("Specialty.NotFound", "Não foi possivel encontrar a especialidade.", ErrorType.NotFound);
        public static Error NameAlredyExists => new("Specialty.Name.AlreadyExists", "Uma especialidae com esse nome já existe.", ErrorType.Conflict);
    }

    public static class PrescribingInformation
    {
        public static Error PrescribingInformationEntityNotFound => 
            new("PrescribingInformation.NotFound", "Não foi possivel encontrar a bula farmacêutica.", ErrorType.NotFound);
    }

    public static class Classification
    {
        public static Error ClassificationsPendingNotFound => 
            new("Classification.PendingNotFound", "Não foi possivel encontrar nenhuma classificação pendente.", ErrorType.NotFound);
        
        public static Error ClassificationsCompletedNotFound => 
            new("Classification.PendingNotFound", "Não foi possivel encontrar nenhuma classificação completa.", ErrorType.NotFound);

        public static Error ClassificationAlreadyExists =>
            new("Classification.AlreadyExists", "A Classificação já existe.", ErrorType.NotFound);
    }

    public static class EmailSend
    {
        public static Error FailedToSendEmail => new("EmailSend.Failure", "Não foi possivel enviar o email.", ErrorType.Failure);
    }

}
