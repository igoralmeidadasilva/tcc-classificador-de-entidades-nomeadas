using Classificador.Api.Infrastructure.Context.Configurations.Abstractions;

namespace Classificador.Api.Infrastructure.Context.Configurations;

public sealed class CategoryConfiguration : SoftDeletableEntityConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.ToTable("categorias");

        builder.Property(x => x.Name)
            .HasColumnName("nome")
            .HasMaxLength(Constants.Constraints.Category.NAME_MAX_LENGHT)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnName("descricao");

        builder.HasMany(x => x.Classifications)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.IdCategory)
            .IsRequired();
    }
}