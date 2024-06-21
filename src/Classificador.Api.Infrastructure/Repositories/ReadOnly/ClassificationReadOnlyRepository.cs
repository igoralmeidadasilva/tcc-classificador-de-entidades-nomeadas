
namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public class ClassificationReadOnlyRepository : BaseReadOnlyRepository<Classification>, IClassificationReadOnlyRepository
{
    public ClassificationReadOnlyRepository(ClassifierContext context) : base(context)
    {
    }

}
