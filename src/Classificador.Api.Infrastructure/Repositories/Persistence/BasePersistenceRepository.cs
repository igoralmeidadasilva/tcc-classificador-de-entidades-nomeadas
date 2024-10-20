using Classificador.Api.Domain.Core.Abstractions;
using Classificador.Api.Domain.Core.Interfaces.Repositories.Persistence;

namespace Classificador.Api.Infrastructure.Repositories.Persistence;

public abstract class BasePersistenceRepository<TEntity> : IPersistenceRepository<TEntity> where TEntity : Entity<TEntity>
{
    protected readonly IDbContextFactory<MedTaggerContext> _contextFactory;
    
    public BasePersistenceRepository(IDbContextFactory<MedTaggerContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Set<TEntity>().Add(entity);
        await context.SaveChangesAsync(cancellationToken);
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
