using Classificador.Api.Domain.Core.Enums;

namespace Classificador.Api.Domain.Entities;

public sealed class Classification : Entity<Classification>
{
    public string? Comment { get; private set; }
    public ClassificationStatus Status { get; private set; }
    public Guid IdNamedEntity { get; private set; }
    public Guid IdCategory { get; private set; }
    public Guid IdUser { get; private set; }
    public NamedEntity? NamedEntity { get; set; }
    public Category? Category { get; set; }
    public User? User { get; set; }

    public Classification() : base() {} //ORM

    private Classification(Guid id, DateTime createdOnUtc, Guid idNamedEntity, Guid idCategory, Guid idUser, string? comment) : base(id, createdOnUtc)
    {
        IdNamedEntity = idNamedEntity;
        IdCategory = idCategory;
        IdUser = idUser;
        Comment = comment;
        Status = ClassificationStatus.Pendente;
    }

    public static Classification Create(Guid idNamedEntity, Guid idCategory, Guid idUser, string? comment)
    {
        ArgumentValidator.ThrowIfNullOrDefault(idNamedEntity, nameof(IdNamedEntity));
        ArgumentValidator.ThrowIfNullOrDefault(idCategory, nameof(IdCategory));
        ArgumentValidator.ThrowIfNullOrDefault(idUser, nameof(IdUser));
        ArgumentValidator.ThrowIfNull(comment!, nameof(Comment));
        
        return new(Guid.NewGuid(), DateTime.UtcNow, idNamedEntity, idCategory, idUser, comment);
    }

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
