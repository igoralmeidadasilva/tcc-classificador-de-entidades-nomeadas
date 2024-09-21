namespace Classificador.Api.Infrastructure.Repositories.Persistence;

public sealed class SpecialtyPersistenceRepository : BasePersistenceRepository<Specialty>, ISpecialtyPersistenceRepository
{
    public SpecialtyPersistenceRepository(IDbContextFactory<ClassifierContext> contextFactory) : base(contextFactory)
    {
    }
}