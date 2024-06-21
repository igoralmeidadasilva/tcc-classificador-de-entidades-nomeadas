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

    public Classification(Guid idNamedEntitie, Guid idCategory, Guid idUser, string? comment="") : base()
    {
        ArgumentValidator.ThrowIfNullOrDefault(idNamedEntitie, nameof(idNamedEntitie));
        ArgumentValidator.ThrowIfNullOrDefault(idCategory, nameof(idCategory));
        ArgumentValidator.ThrowIfNullOrDefault(idUser, nameof(idUser));
        ArgumentValidator.ThrowIfNull(comment!, nameof(comment));

        IdNamedEntitie = idNamedEntitie;
        IdCategory = idCategory;
        IdUser = idUser;
        Comment = comment;
    }

    public override Classification Update(Classification entity)
    {
        IdNamedEntitie = entity.IdNamedEntitie;
        IdCategory = entity.IdCategory;
        IdUser = entity.IdUser;
        Comment = entity.Comment;

        return this;
    }

}
