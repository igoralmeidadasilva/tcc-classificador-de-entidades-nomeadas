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
        builder.Entity<Category>().HasQueryFilter(c => !c.IsDeleted);
        builder.Entity<Classification>().HasQueryFilter(c => !c.IsDeleted);
        builder.Entity<NamedEntity>().HasQueryFilter(n => !n.IsDeleted);
        builder.Entity<PrescribingInformation>().HasQueryFilter(p => !p.IsDeleted);
        builder.Entity<Specialty>().HasQueryFilter(s => !s.IsDeleted);
        builder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<NamedEntityPrescribingInformation>().HasQueryFilter(npi  => 
            !npi.NamedEntity!.IsDeleted && 
            !npi .PrescribingInformation!.IsDeleted);

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
