namespace Classificador.Api.SharedKernel.Shared;

public sealed record Error
{
    public string Code { get; set; }
    public string Message { get; set; }
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
    public static readonly Error None = new(string.Empty, string.Empty);
}
