using Classificador.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Classificador.Api.Infrastructure.Context.Configurations;

public sealed class ClassificationConfiguration : IEntityTypeConfiguration<Classification>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Classification> builder)
    {
        throw new NotImplementedException();
    }

}
