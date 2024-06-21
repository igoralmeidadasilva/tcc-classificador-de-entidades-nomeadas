namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public class BaseReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : Entity<TEntity>
{
    private readonly ClassifierContext _context;

    public BaseReadOnlyRepository(ClassifierContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return (await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken))!;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().AsNoTracking().AnyAsync(x => x.Id == id, cancellationToken);
    }

}
