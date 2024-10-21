using Classificador.Api.Domain.Core.Abstractions;
using Classificador.Api.Domain.Core.Interfaces;

namespace Classificador.Api.Infrastructure.Context.Configurations.Abstractions;

public abstract class SoftDeletableEntityConfiguration<TEntity> : EntityConfiguration<TEntity>
    where TEntity : Entity<TEntity>, ISoftDeletableEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.IsDeleted)
            .HasColumnName("foi_deletado")
            .IsRequired();

        builder.Property(e => e.DeletedOnUtc)
            .HasColumnName("data_remocao_utc")
            .IsRequired(false);
    }
}

