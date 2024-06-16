namespace Classificador.Api.Infrastructure.Context.Configurations;

public sealed class CategoryConfiguration : EntityConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.ToTable("categorias");

        builder.Property(x => x.Name)
            .HasColumnName("nome")
            .HasMaxLength(Constants.Constraints.CATEGORYS_NAME_MAX_LENGHT)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnName("descricao");

        builder.HasMany(x => x.Classifications)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.IdCategory)
            .IsRequired();
    }
}