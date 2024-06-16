namespace Classificador.Api.Infrastructure.Context.Configurations;

public sealed class NamedEntityPrescribingInformationConfiguration : IEntityTypeConfiguration<NamedEntityPrescribingInformation>
{
    public void Configure(EntityTypeBuilder<NamedEntityPrescribingInformation> builder)
    {
        builder.ToTable("entidades_nomeadas_por_bula");

        builder.HasKey(x => new { x.IdPrescribingInformation, x.IdNamedEntity });

        builder.Property(x => x.IdPrescribingInformation)
            .HasColumnName("id_bula");

        builder.HasOne(x => x.PrescribingInformation)
            .WithMany(x => x.NamedEntityPrescribingsInformation)
            .HasForeignKey(x => x.IdPrescribingInformation)
            .IsRequired();

        builder.Property(x => x.IdNamedEntity)
            .HasColumnName("id_entidade_nomeada");

        builder.HasOne(be => be.NamedEntity)
            .WithMany(e => e.NamedEntityPrescribingsInformation)
            .HasForeignKey(be => be.IdNamedEntity)
            .IsRequired();
    }

}
