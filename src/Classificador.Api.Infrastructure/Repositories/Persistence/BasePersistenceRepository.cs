namespace Classificador.Api.Infrastructure.Repositories.Persistence;

public abstract class BasePersistenceRepository<TEntity> : IPersistenceRepository<TEntity> where TEntity : Entity<TEntity>
{
    protected readonly IDbContextFactory<ClassifierContext> _contextFactory;
    
    public BasePersistenceRepository(IDbContextFactory<ClassifierContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<Guid> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Set<TEntity>().Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Set<TEntity>().AddRange(entities);
        await context.SaveChangesAsync(cancellationToken);        
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        var entity = await context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        context.Set<TEntity>().Remove(entity!);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Set<TEntity>().Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}
