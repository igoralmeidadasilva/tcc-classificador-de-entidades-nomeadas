using Classificador.Api.Infrastructure.Context.Configurations.Abstractions;

namespace Classificador.Api.Infrastructure.Context.Configurations;

public sealed class NamedEntityConfiguration : SoftDeletableEntityConfiguration<NamedEntity>
{
    public override void Configure(EntityTypeBuilder<NamedEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("entidades_nomeadas");

        builder.Property(x => x.Name)
            .HasColumnName("nome")
            .HasMaxLength(Constants.Constraints.NamedEntity.NAME_MAX_LENGHT)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnName("descricao");

        builder.HasMany(x => x.Classifications)
            .WithOne(x => x.NamedEntity)
            .HasForeignKey(x => x.IdNamedEntity)
            .IsRequired();

        builder.Property(x => x.IdPrescribingInformation)
            .HasColumnName("id_bula")
            .IsRequired();
        
        builder.HasOne(x => x.PrescribingInformation)
            .WithMany(x => x.NamedEntities)
            .HasForeignKey(x => x.IdPrescribingInformation);
        
        builder.OwnsOne(x => x.WordPosition)
            .Property(x => x.StartPosition)
            .HasColumnName("posicao_inicial")
            .IsRequired();

        builder.OwnsOne(x => x.WordPosition)
            .Property(x => x.EndPosition)
            .HasColumnName("posicao_final")
            .IsRequired();
    }
}
