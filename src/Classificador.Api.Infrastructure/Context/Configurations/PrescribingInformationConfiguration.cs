using Classificador.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Classificador.Api.Infrastructure.Context.Configurations;

public class PrescribingInformationConfiguration : IEntityTypeConfiguration<PrescribingInformation>
{
    public void Configure(EntityTypeBuilder<PrescribingInformation> builder)
    {
        throw new NotImplementedException();
    }

}
