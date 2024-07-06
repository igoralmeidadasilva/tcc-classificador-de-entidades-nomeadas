namespace Classificador.Api.Infrastructure.Repositories.Persistence;

public class BasePersistenceRepository<TEntity> : IPersistenceRepository<TEntity> where TEntity : Entity<TEntity>
{
    private readonly ClassifierContext _context;

    public BasePersistenceRepository(ClassifierContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().AddRange(entities);
        await _context.SaveChangesAsync(cancellationToken);        
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        // TODO: Verificar esse tipo de retorno, lançando exceções de dentro do repo
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            ?? throw new EntityNotFoundException();
        
        _context.Set<TEntity>().Remove(entity!);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
