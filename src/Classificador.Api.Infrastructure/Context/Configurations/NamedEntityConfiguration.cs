using Classificador.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Classificador.Api.Infrastructure.Context.Configurations;

public sealed class NamedEntityConfiguration : IEntityTypeConfiguration<NamedEntity>
{
    public void Configure(EntityTypeBuilder<NamedEntity> builder)
    {
        throw new NotImplementedException();
    }

}
