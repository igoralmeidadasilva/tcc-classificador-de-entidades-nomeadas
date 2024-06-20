namespace Classificador.Api.Domain.Entities;

public sealed class Classification : Entity<Classification>, IAggregateRoot
{
    public string? Comment { get; private set; }
    public Guid IdNamedEntitie { get; private set; }
    public Guid IdCategory { get; private set; }
    public Guid IdUser { get; private set; }
    public NamedEntity? NamedEntitie { get; set; }
    public Category? Category { get; set; }
    public User? User { get; set; }

    public Classification(Guid idNamedEntitie, Guid idCategory, Guid idUser, string? comment = "") : base()
    {
        IdNamedEntitie = idNamedEntitie;
        IdCategory = idCategory;
        IdUser = idUser;
        Comment = comment;
    }

    public override Classification Update(Classification entity)
    {
        throw new NotImplementedException();
    }

}
