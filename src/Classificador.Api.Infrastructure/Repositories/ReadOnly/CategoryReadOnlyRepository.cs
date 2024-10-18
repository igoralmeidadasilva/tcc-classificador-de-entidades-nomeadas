using Classificador.Api.Domain.Core.Interfaces.Repositories.ReadOnly;

namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public sealed class CategoryReadOnlyRepository : BaseReadOnlyRepository<Category>, ICategoryReadOnlyRepository
{
    public CategoryReadOnlyRepository(IDbContextFactory<ClassifierContext> contextFactory) : base(contextFactory)
    {
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Categories.AsNoTracking().AnyAsync(x => x.Name == name, cancellationToken);
    }

    public new async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Categories.AsNoTracking().OrderBy(x => x.Name).ToListAsync(cancellationToken);
    }
    
}
