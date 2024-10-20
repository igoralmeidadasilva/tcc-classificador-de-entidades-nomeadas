using Classificador.Api.Infrastructure.Context.Configurations.Abstractions;

namespace Classificador.Api.Infrastructure.Context.Configurations;

public sealed class SpecialtyConfiguration : SoftDeletableEntityConfiguration<Specialty>
{
    public override void Configure(EntityTypeBuilder<Specialty> builder)
    {
        base.Configure(builder);

        builder.ToTable("especialidades");

        builder.Property(x => x.Name)
            .HasColumnName("nome")
            .HasMaxLength(Constants.Constraints.Specialty.NAME_MAX_LENGHT)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnName("descricao");

        builder.HasMany(x => x.Users)
            .WithOne(x => x.Specialty)
            .HasForeignKey(x => x.IdSpecialty)
            .IsRequired();
    }

}
