namespace Classificador.Api.Presentation.Models;

public sealed record ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
