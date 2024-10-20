namespace Classificador.Api.Domain.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    void Commit();
    void Rollback();
    void SaveChanges();
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
