namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public sealed class CategoryReadOnlyRepository : BaseReadOnlyRepository<Category>, ICategoryReadOnlyRepository
{
    public CategoryReadOnlyRepository(ClassifierContext context) : base(context)
    {
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Categories.AsNoTracking().AnyAsync(x => x.Name == name, cancellationToken);
    }

    public new async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Categories.AsNoTracking().OrderBy(x => x.Name).ToListAsync(cancellationToken);
    }
    
}
