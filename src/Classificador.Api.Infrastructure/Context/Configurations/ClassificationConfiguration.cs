namespace Classificador.Api.Infrastructure.Context.Configurations;

public sealed class ClassificationConfiguration : EntityConfiguration<Classification>
{
    public override void Configure(EntityTypeBuilder<Classification> builder)
    {
        base.Configure(builder);

        builder.ToTable("classificacoes");
        
        builder.Property(x => x.Comment)
            .HasColumnName("comentarios");

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion<string>();

        builder.Property(x => x.IdCategory)
            .HasColumnName("id_categoria")
            .IsRequired();

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Classifications)
            .HasForeignKey(x => x.IdCategory)
            .IsRequired();

        builder.Property(x => x.IdNamedEntity)
            .HasColumnName("id_entidade_nomeada")
            .IsRequired();

        builder.HasOne(x => x.NamedEntity)
            .WithMany(x => x.Classifications)
            .HasForeignKey(x => x.IdNamedEntity)
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
