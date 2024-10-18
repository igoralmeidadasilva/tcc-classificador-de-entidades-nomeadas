using Classificador.Api.Domain.Core.Interfaces.Repositories.Persistence;

namespace Classificador.Api.Infrastructure.Repositories.Persistence;

public sealed class PrescribingInformationPersistenceRepository
    : BasePersistenceRepository<PrescribingInformation>, IPrescribingInformationPersistenceRepository
{
    public PrescribingInformationPersistenceRepository(IDbContextFactory<ClassifierContext> contextFactory) : base(contextFactory)
    {
    }

}
