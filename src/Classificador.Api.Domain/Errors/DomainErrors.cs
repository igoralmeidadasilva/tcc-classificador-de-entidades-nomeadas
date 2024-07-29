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
        public static Error EmailAlreadyExists => new("User.Email.AlreadyExists", "O Email já existe.", ErrorType.Conflict);
        public static Error AuthenticationPasswordFailed => new("User.AuthenticationPassword.Failed", "Credênciais Inválidas.", ErrorType.Unauthorized);
    }

    public static class NamedEntity
    {
        public static Error NamedEntityNotFound => new("NamedEntity.NotFound", "Não foi possivel encontrar a entidade nomeada.", ErrorType.NotFound);
    }

    public static class Category
    {
        public static Error CategoryEntityNotFound => new("Category.NotFound", "Não foi possivel encontrar a categoria.", ErrorType.NotFound);
        public static Error NameAlredyExists => new("Category.Name.AlreadyExists", "Uma categoria com esse nome já existe.", ErrorType.Conflict);
    }

    public static class Specialty
    {
        public static Error SpecialtyEntityNotFound => new("Specialty.NotFound", "Não foi possivel encontrar a especialidade.", ErrorType.NotFound);
        public static Error NameAlredyExists => new("Specialty.Name.AlreadyExists", "Uma especialidae com esse nome já existe.", ErrorType.Conflict);
    }

    public static class EmailSend
    {
        public static Error FailedToSendEmail => new("EmailSend.Failure", "Não foi possivel enviar o email.", ErrorType.Failure);
    }
}
