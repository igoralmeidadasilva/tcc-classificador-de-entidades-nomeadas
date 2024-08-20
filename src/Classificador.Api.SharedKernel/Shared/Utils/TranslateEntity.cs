namespace Classificador.Api.SharedKernel.Shared.Utils;

// FIXME: Unused class
public static class TranslateEntity
{
    public static string Translate(string entity)
    {
        return entity switch
        {
            "Category" => "Categoria",
            "Classification" => "Classificação",
            "NamedEntity" => "Entidade Nomeada",
            "PrescribingInformation" => "Bula",
            "Specialty" => "Especialidae",
            "User" => "Usuário",
            _ => string.Empty,
        };
    }
}