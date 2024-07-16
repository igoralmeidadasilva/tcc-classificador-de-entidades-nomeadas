namespace Classificador.Api.Domain.Errors;

public static class DomainErrors
{
    public static class User
    {
        public static Error UserNotFound => new("User.NotFound", "Não foi possivel encontrar o usuário.", ErrorType.NotFound);
        public static Error EmailAlreadyExists => new("User.Email.AlreadyExists", "O Email já existe.", ErrorType.Conflict);
        public static Error AuthenticationPasswordFailed => new("User.AuthenticationPassword.Failed", "Credênciais Inválidas.", ErrorType.Unauthorized);
    }
}
