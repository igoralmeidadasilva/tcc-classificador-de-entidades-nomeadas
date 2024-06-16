namespace Classificador.Api.Infrastructure.Context.Configurations;

public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity<TEntity>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(x => x.CreatedAtOnUtc)
            .HasColumnName("data_criacao")
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasColumnName("foi_deletado")
            .IsRequired();          
    }
}
