namespace Classificador.Api.Infrastructure.Context.Configurations;

public sealed class ClassificationConfiguration : EntityConfiguration<Classification>
{
    public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Classification> builder)
    {
        base.Configure(builder);

        builder.ToTable("classificacoes");
        
        builder.Property(x => x.Comment)
            .HasColumnName("comentarios");

        builder.Property(x => x.IdCategory)
            .HasColumnName("id_categoria")
            .IsRequired();

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Classifications)
            .HasForeignKey(x => x.IdCategory)
            .IsRequired();

        builder.Property(x => x.IdNamedEntitie)
            .HasColumnName("id_entidade_nomeada")
            .IsRequired();

        builder.HasOne(x => x.NamedEntitie)
            .WithMany(x => x.Classifications)
            .HasForeignKey(x => x.IdNamedEntitie)
            .IsRequired();

        builder.Property(x => x.IdUser)
            .HasColumnName("id_usuario")
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Classifications)
            .HasForeignKey(x => x.IdUser)
            .IsRequired();
    
    }

}
