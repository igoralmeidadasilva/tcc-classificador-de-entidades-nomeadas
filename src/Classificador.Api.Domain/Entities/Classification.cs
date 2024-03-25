using Classificador.Api.Domain.Interfaces;

namespace Classificador.Api.Domain.Entities;

public sealed record Classification : BaseEntity<Classification>, IEntity<Classification>, IAggregateRoot
{
    public string? Comment { get; private set; }
    public Guid IdNamedEntitie { get; private set; }
    public Guid NameCategory { get; private set; }
    public Guid IdUser { get; private set; }
    public NamedEntity? NamedEntitie { get; set; }
    public Category? Category{ get; set; }
    public User? User{ get; set; }

    public Classification(Guid idNamedEntitie, Guid nameCategory, Guid idUser, string? comment = "") : base(Guid.NewGuid())
    {
        IdNamedEntitie = idNamedEntitie;
        NameCategory = nameCategory;
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
