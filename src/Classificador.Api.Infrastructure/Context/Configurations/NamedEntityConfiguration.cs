namespace Classificador.Api.Infrastructure.Context.Configurations;

public sealed class NamedEntityConfiguration : EntityConfiguration<NamedEntity>
{
    public override void Configure(EntityTypeBuilder<NamedEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("entidade_nomeada");

        builder.Property(x => x.Name)
            .HasColumnName("nome")
            .HasMaxLength(Constants.Constraints.NAMED_ENTITY_NAME_MAX_LENGHT)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnName("descricao");

        builder.HasMany(x => x.Classifications)
            .WithOne(x => x.NamedEntitie)
            .HasForeignKey(x => x.IdNamedEntitie)
            .IsRequired();
    }
}
