namespace Classificador.Api.Domain.Errors;

public static class BaseErrors
{
    public static Error EntityNotFound(string entityName) => new Error(
        "BaseEntity.NotFound", $"{entityName} não encontrado");
}
