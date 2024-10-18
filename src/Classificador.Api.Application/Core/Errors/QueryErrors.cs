namespace Classificador.Api.Application.Core.Errors;

public static class QueryErrors
{
    public static class GetPendingClassificationsFailures
    {
        public static Error UserIdIsRequired =>
            Error.Create("GetPendingClassifications.IdUser.IsRequired", "O Id do usuário não pode ser vazio.", ErrorType.Validation);
        public static Error PrescribingInformationIdIsRequired =>
            Error.Create("GetPendingClassifications.IdPrescribingInformation.IsRequired", "O Id da bula não pode ser vazia.", ErrorType.Validation);
    }

    public static class GetAllClassificationByVotesFailures
    {
        public static Error PrescribingInformationIdIsRequired =>
            Error.Create("GetAllClassificationByVotesFailures.IdPrescribingInformation.IsRequired", "O Id da bula não pode ser vazia.", ErrorType.Validation);
    }

    public static class GetNamedEntityByPrescribingInformationIdFailures
    {
        public static Error PrescribingInformationIdIsRequired => Error.Create(
            "GetNamedEntityByPrescribingInformationIdQueryValidator.IdPrescribingInformation.IsRequired",
            "O Id da bula não pode ser vazia.",
            ErrorType.Validation);

        public static Error UserIdIsRequired =>
            Error.Create("GetNamedEntityByPrescribingInformationIdQueryValidator.IdUser.IsRequired", "O Id do usuário não pode ser vazio.", ErrorType.Validation);
    }

}
