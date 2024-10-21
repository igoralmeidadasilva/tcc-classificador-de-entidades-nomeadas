namespace Classificador.Api.Domain.Core.Interfaces;

public interface ISoftDeletableEntity
{
    public bool IsDeleted { get; }
    public DateTime? DeletedOnUtc { get; }
    public void Delete();
    public void Restore();
}