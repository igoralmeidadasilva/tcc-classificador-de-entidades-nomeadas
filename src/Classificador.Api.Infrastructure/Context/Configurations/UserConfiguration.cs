using Classificador.Api.Infrastructure.Context.Configurations.Abstractions;

namespace Classificador.Api.Infrastructure.Context.Configurations;

public sealed class UserConfiguration : SoftDeletableEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("usuarios");

        builder.Property(x => x.Email)
            .HasColumnName("email")
            .HasMaxLength(Constants.Constraints.User.EMAIL_MAX_LENGHT)
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.HashedPassword)
            .HasColumnName("senha")
            .HasMaxLength(Constants.Constraints.User.PASSWORD_MAX_LENGHT)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("nome")
            .HasMaxLength(Constants.Constraints.User.NAME_MAX_LENGHT)
            .IsRequired();

        builder.Property(x => x.Contact)
            .HasColumnName("contato")
            .HasMaxLength(Constants.Constraints.User.CONTACT_MAX_LENGHT);
        
        builder.Property(x => x.Role)
            .HasColumnName("funcao")
            .HasConversion<string>();

        builder.Property(x => x.IdSpecialty)
            .HasColumnName("id_especialidade")
            .IsRequired(false);

        builder.HasOne(x => x.Specialty)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.IdSpecialty);

        builder.HasMany(x => x.Classifications)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.IdUser)
            .IsRequired();
    }
}
