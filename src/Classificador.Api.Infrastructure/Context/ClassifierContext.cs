namespace Classificador.Api.Infrastructure.Context;

public sealed class ClassifierContext : DbContext
{
    DbSet<NamedEntity> NamedEntities { get; init; }
    DbSet<NamedEntityPrescribingInformation> NamedEntityPrescribingsInformation { get; init; }
    DbSet<PrescribingInformation> PrescribingsInformation { get; init; }
    DbSet<User> Users { get; init; }
    DbSet<Specialty> Specialties{ get; init; }
    DbSet<Classification> Classifications { get; init; }
    DbSet<Category> Categories { get; init; }

    public ClassifierContext(DbContextOptions options) : base(options) { }
    public ClassifierContext() {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new ClassificationConfiguration());
        builder.ApplyConfiguration(new NamedEntityConfiguration());
        builder.ApplyConfiguration(new NamedEntityPrescribingInformationConfiguration());
        builder.ApplyConfiguration(new PrescribingInformationConfiguration());
        builder.ApplyConfiguration(new SpecialtyConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        
        base.OnModelCreating(builder);
    }

}
