using Classificador.Api.Domain.Core.Interfaces;

namespace Classificador.Api.Infrastructure.Context;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly MedTaggerContext _context;

    public UnitOfWork(MedTaggerContext context)
    {
        _context = context;
    }

    public void BeginTransaction() => _context.Database.BeginTransaction();

    public void Commit() => _context.Database.CommitTransaction();

    public void Dispose() => _context?.Dispose();

    public void Rollback() => _context.Database.RollbackTransaction();

    public void SaveChanges() => _context?.SaveChanges();

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
}
