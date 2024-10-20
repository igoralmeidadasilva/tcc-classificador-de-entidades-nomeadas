using Classificador.Api.Domain.Core.Interfaces.Repositories.Persistence;

namespace Classificador.Api.Infrastructure.Repositories.Persistence;

public sealed class SpecialtyPersistenceRepository : BasePersistenceRepository<Specialty>, ISpecialtyPersistenceRepository
{
    public SpecialtyPersistenceRepository(IDbContextFactory<MedTaggerContext> contextFactory) : base(contextFactory)
    {
    }
}