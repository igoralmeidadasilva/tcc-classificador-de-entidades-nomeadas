
namespace Classificador.Api.Infrastructure.Repositories.Persistence;

public sealed class ClassificationPersistenceRepository : BasePersistenceRepository<Classification>, IClassificationPersistenceRepository
{
    public ClassificationPersistenceRepository(ClassifierContext context) : base(context)
    {
    }

}
