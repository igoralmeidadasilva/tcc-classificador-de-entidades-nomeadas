namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public sealed class PrescribingInformationReadOnlyRepository
    : BaseReadOnlyRepository<PrescribingInformation>, IPrescribingInformationReadOnlyRepository
{
    public PrescribingInformationReadOnlyRepository(ClassifierContext context) : base(context)
    {
    }

}
