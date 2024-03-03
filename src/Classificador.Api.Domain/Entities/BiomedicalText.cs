namespace Classificador.Api.Domain.Entities;

public class BiomedicalText : EntityBase<BiomedicalText>
{
    public string Content { get; private set; } = String.Empty;
}
