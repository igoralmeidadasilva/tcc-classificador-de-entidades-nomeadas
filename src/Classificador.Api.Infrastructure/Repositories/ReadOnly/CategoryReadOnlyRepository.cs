namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public sealed class CategoryReadOnlyRepository : BaseReadOnlyRepository<Category>, ICategoryReadOnlyRepository
{
    public CategoryReadOnlyRepository(ClassifierContext context) : base(context)
    {
    }

}
