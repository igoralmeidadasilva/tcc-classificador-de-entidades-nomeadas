namespace Classificador.Api.Infrastructure.Context;

public sealed class ClassifierContext : DbContext
{
    public DbSet<NamedEntity> NamedEntities { get; init; }
    public DbSet<NamedEntityPrescribingInformation> NamedEntityPrescribingsInformation { get; init; }
    public DbSet<PrescribingInformation> PrescribingsInformation { get; init; }
    public DbSet<User> Users { get; init; }
    public DbSet<Specialty> Specialties{ get; init; }
    public DbSet<Classification> Classifications { get; init; }
    public DbSet<Category> Categories { get; init; }

    public ClassifierContext(DbContextOptions options) : base(options) { }
    public ClassifierContext(DbContextOptionsBuilder options) {}

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
