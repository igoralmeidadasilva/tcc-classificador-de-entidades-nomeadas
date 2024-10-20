namespace Classificador.Api.Presentation;

// TODO: Remover e colocar na camada de domain
public static class Constants
{
    public static class Messages
    {
        public const string SignUpSuccess = "Cadastro realizado com sucesso.";
        public const string AccessDenied = "Acesso não autorizado. Por favor faça Login para continuar.";
        public const string ClassificationSuccessfully = "A entidade foi classificada com sucesso.";
        public const string ClassificationIsDone = "Essa bula não tem mais entidades para você classificar.";
        public const string DeletePendingClassificationSuccessfully = "A classificação foi removida com sucesso.";
        public const string CreatePrescribingInformationSuccessfully = "A bula foi adicionada com sucesso.";
    }
}