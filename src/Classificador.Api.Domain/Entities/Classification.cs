namespace Classificador.Api.Domain.Entities;

public sealed class Classification : Entity<Classification>, IAggregateRoot
{
    public string? Comment { get; private set; }
    public ClassificationStatus Status { get; private set; }
    public Guid IdNamedEntity { get; private set; }
    public Guid IdCategory { get; private set; }
    public Guid IdUser { get; private set; }
    public NamedEntity? NamedEntity { get; set; }
    public Category? Category { get; set; }
    public User? User { get; set; }

    public Classification(Guid idNamedEntity, Guid idCategory, Guid idUser, string? comment) : base()
    {
        ArgumentValidator.ThrowIfNullOrDefault(idNamedEntity, nameof(idNamedEntity));
        ArgumentValidator.ThrowIfNullOrDefault(idCategory, nameof(idCategory));
        ArgumentValidator.ThrowIfNullOrDefault(idUser, nameof(idUser));
        ArgumentValidator.ThrowIfNull(comment!, nameof(comment));

        IdNamedEntity = idNamedEntity;
        IdCategory = idCategory;
        IdUser = idUser;
        Comment = comment;
        Status = ClassificationStatus.Pendente;
    }

    public Classification() {} //ORM

    public override Classification Update(Classification entity)
    {
        IdNamedEntity = entity.IdNamedEntity;
        IdCategory = entity.IdCategory;
        IdUser = entity.IdUser;
        Comment = entity.Comment;

        return this;
    }

    public Classification UpdateToComplete()
    {
        Status = ClassificationStatus.Completo;

        return this;
    }
}
