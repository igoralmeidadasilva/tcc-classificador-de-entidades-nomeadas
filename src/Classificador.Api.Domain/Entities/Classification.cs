using Classificador.Api.Domain.Interfaces;

namespace Classificador.Api.Domain.Entities;

public sealed record Classification : Entity<Classification>, IAggregateRoot
{
    public string? Comment { get; private set; }
    public Guid IdNamedEntitie { get; private set; }
    public Guid IdNameCategory { get; private set; }
    public Guid IdUser { get; private set; }
    public NamedEntity? NamedEntitie { get; set; }
    public Category? Category{ get; set; }
    public User? User{ get; set; }

    public Classification(Guid idNamedEntitie, Guid idNameCategory, Guid idUser, string? comment = "") : base()
    {
        IdNamedEntitie = idNamedEntitie;
        IdNameCategory = idNameCategory;
        IdUser = idUser;
        Comment = comment;
    }

    public override Classification Update(Classification entity)
    {
        throw new NotImplementedException();
    }

    public override void Validate()
    {
        throw new NotImplementedException();
    }
}
