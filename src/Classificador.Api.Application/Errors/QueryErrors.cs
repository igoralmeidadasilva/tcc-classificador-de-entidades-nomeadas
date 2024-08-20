namespace Classificador.Api.Application.Errors;

public static class QueryErrors
{
    public static class GetPendingClassificationsFailures
    {
        public static PropertyFailure UserIdIsRequired => new("GetPendingClassifications.IdUser.IsRequired", "O Id do usuário não pode ser vazio.");
        public static PropertyFailure PrescribingInformationIdIsRequired => 
            new("GetPendingClassifications.IdPrescribingInformation.IsRequired", "O Id da bula não pode ser vazia.");
    }

    public static class GetAllClassificationByVotesFailures
    {
        public static PropertyFailure PrescribingInformationIdIsRequired => 
            new("GetAllClassificationByVotesFailures.IdPrescribingInformation.IsRequired", "O Id da bula não pode ser vazia.");
    }
}
