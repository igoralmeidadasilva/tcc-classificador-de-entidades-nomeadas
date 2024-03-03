namespace Classificador.Api.Domain.Entities;

public sealed class Classification : EntityBase<Classification>
{
    public int IdNamedEntitie { get; init; }
    public int IdSpecialist { get; init; }
    public int Vote { get; private set; }
    public DateTime VoteDate{ get; private set; }
}
