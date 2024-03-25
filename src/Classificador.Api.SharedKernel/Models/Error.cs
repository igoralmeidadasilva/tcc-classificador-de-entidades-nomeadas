namespace Classificador.Api.SharedKernel.Models;

public sealed class Error
{
    public string Code { get; set; }
    public string Description { get; set; }
    public Error(string code, string description)
    {
        Code = code;
        Description = description;
    }
    public static readonly Error None = new(string.Empty, string.Empty);
}
