using Classificador.Api.Domain.Core.Abstractions;

namespace Classificador.Api.Infrastructure.Context.Configurations.Abstractions;

public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity<TEntity>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.Property(x => x.CreatedOnUtc)
            .HasColumnName("data_criacao_utc")
            .IsRequired();

        // builder.Property(x => x.IsDeleted)
        //     .HasColumnName("foi_deletado")
        //     .IsRequired();         
    }
}
