namespace Classificador.Api.Domain;

public static class Constants
{
    public static class Constraints
    {
        public static class User
        {
            public const int EMAIL_MAX_LENGHT = 45;
            public const int NAME_MAX_LENGHT = 45;
            public const int PASSWORD_MIN_LENGHT = 8;
            public const int PASSWORD_MAX_LENGHT = 64;
            public const string PASSWORD_FORMAT = @"^(?=.*[!@#$%^&*()_+\[\]{}|\\:;'"",.<>?/~]).*$";
            public const int CONTACT_MAX_LENGHT = 15;
            public const string CONTACT_FORMAT = @"^\(\d{2}\) \d{5}-\d{4}$";
        }

        public static class PrescribingInformation
        {
            public const int NAME_MAX_LENGHT = 45;
        }
        public static class NamedEntity
        {
            public const int NAME_MAX_LENGHT = 45;
        }
        public static class Category
        {
            public const int NAME_MAX_LENGHT = 45;
        }
        public static class Specialty
        {
            public const int NAME_MAX_LENGHT = 45;
        }
    }

    public static class Messages
    {
        public const string InvalidForm = "Ocorreu um erro ao enviar o formulário, por favor revise seus dados, se o error persistir, entre em contato.";
        public const string SendSuccessfully = "A sua mensagem foi enviada com sucesso. Em breve alguem entrara em contato pelo email fornecido.";
        public const string SignUpSuccess = "Cadastro realizado com sucesso.";
        public const string AccessDenied = "Acesso não autorizado. Por favor faça Login para continuar.";
        public const string ClassificationSuccessfully = "A entidade foi classificada com sucesso.";
        public const string ClassificationIsDone = "Essa bula não tem mais entidades para você classificar.";
        public const string DeletePendingClassificationSuccessfully = "A classificação foi removida com sucesso.";
        public const string CreatePrescribingInformationSuccessfully = "A bula foi adicionada com sucesso.";
    }
}
