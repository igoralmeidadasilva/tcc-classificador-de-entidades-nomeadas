namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public abstract class BaseReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : Entity<TEntity>
{
    protected readonly IDbContextFactory<ClassifierContext> _contextFactory;

    public BaseReadOnlyRepository(IDbContextFactory<ClassifierContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        return (await context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken))!;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Set<TEntity>().AsNoTracking().AnyAsync(x => x.Id == id, cancellationToken);
    }

}
