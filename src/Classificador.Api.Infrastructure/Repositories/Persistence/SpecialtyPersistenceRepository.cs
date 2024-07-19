namespace Classificador.Api.Infrastructure.Repositories.Persistence;

public sealed class SpecialtyPersistenceRepository : BasePersistenceRepository<Specialty>, ISpecialtyPersistenceRepository
{
    public SpecialtyPersistenceRepository(ClassifierContext context) : base(context)
    {
    }
}