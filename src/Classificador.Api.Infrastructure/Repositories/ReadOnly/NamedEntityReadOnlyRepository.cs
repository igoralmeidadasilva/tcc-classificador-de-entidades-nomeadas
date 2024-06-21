
namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public class NamedEntityReadOnlyRepository : BaseReadOnlyRepository<NamedEntity>, INamedEntityReadOnlyRepository
{
    public NamedEntityReadOnlyRepository(ClassifierContext context) : base(context)
    {
    }

}
