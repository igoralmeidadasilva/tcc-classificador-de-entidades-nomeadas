namespace Classificador.Api.Infrastructure.Context.Configurations;

public sealed class SpecialtyConfiguration : EntityConfiguration<Specialty>
{
    public override void Configure(EntityTypeBuilder<Specialty> builder)
    {
        base.Configure(builder);

        builder.ToTable("especialidade");

        builder.Property(x => x.Name)
            .HasColumnName("nome")
            .HasMaxLength(Constants.Constraints.SPECIALTY_NAME_MAX_LENGHT)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnName("descricao");

        builder.HasMany(x => x.Users)
            .WithOne(x => x.Specialty)
            .HasForeignKey(x => x.IdSpecialty)
            .IsRequired();
    }

}
