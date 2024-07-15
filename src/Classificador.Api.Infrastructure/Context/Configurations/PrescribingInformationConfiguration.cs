namespace Classificador.Api.Infrastructure.Context.Configurations;

public class PrescribingInformationConfiguration : EntityConfiguration<PrescribingInformation>
{
    public override void Configure(EntityTypeBuilder<PrescribingInformation> builder)
    {
        base.Configure(builder);

        builder.ToTable("bulas");

        builder.Property(x => x.Name)
            .HasColumnName("nome")
            .HasMaxLength(Constants.Constraints.PRESCRIBING_INFORMATION_NAME_MAX_LENGHT)
            .IsRequired();

        builder.Property(x => x.Text)
            .HasColumnName("texto")
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnName("descricao");

        builder.HasMany(x => x.NamedEntities)
            .WithOne(x => x.PrescribingInformation)
            .HasForeignKey(x => x.IdPrescribingInformation)
            .IsRequired();;
    }

}
