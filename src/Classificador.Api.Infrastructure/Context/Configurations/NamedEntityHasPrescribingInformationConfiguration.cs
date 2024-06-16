using Classificador.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Classificador.Api.Infrastructure.Context.Configurations;

public sealed class NamedEntityHasPrescribingInformationConfiguration : IEntityTypeConfiguration<NamedEntityHasPrescribingInformation>
{
    public void Configure(EntityTypeBuilder<NamedEntityHasPrescribingInformation> builder)
    {
        throw new NotImplementedException();
    }

}
