namespace Classificador.Api.Domain.Errors;

public static class DomainErrors
{
    public static class User
    {
        public static Error UserNotFound => new("User.NotFound", "Não foi possivel encontrar o usuário.");
        public static Error EmailAlreadyExists => new("User.Email.AlreadyExists", "O Email já existe.");
        public static Error AuthenticationPasswordFailed => new("User.AuthenticationPassword.Failed", "Credênciais Inválidas.");
    }
}
