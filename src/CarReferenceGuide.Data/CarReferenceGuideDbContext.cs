using CarReferenceGuide.Data.Domain.Interfaces;
using CarReferenceGuide.Data.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarReferenceGuide.Data;

public class CarReferenceGuideDbContext : DbContext 
{
    public CarReferenceGuideDbContext(DbContextOptions<CarReferenceGuideDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public DbSet<Car> Cars { get; set; } = default!;
    public DbSet<ColorCar> Colors { get; set; } = default!;
    public DbSet<Country> Countries { get; set; } = default!;
    public DbSet<OwnerCar> OwnersCars { get; set; } = default!;
    public DbSet<BrandCar> BrandsCars { get; set; } = default!;
    public DbSet<ModelCar> ModelsCars { get; set; } = default!;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        foreach (var property in entityType.GetProperties())
        {
            var propertyType = Nullable.GetUnderlyingType(property.ClrType) ?? property.ClrType;
            if (propertyType.IsEnum)
                property.SetProviderClrType(typeof(string));
        }

        builder.Entity<Car>()
            .HasOne(e => e.Color)
            .WithMany(e => e.Cars)
            .OnDelete(DeleteBehavior.SetNull);
        builder.Entity<ColorCar>()
            .HasAlternateKey(x => x.Name);
        builder.Entity<Country>()
            .HasAlternateKey(x => x.Name);
        builder.Entity<OwnerCar>()
            .HasAlternateKey(x => x.PassportNumber);
        builder.Entity<ModelCar>()
            .HasAlternateKey(x => x.Name);
        builder.Entity<BrandCar>()
            .HasAlternateKey(x => x.Name);
        
        base.OnModelCreating(builder);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SoftDelete();
        WriteTime();
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    private void SoftDelete()
    {
        foreach (var entry in ChangeTracker.Entries<IHistoricalEntity>())
            switch (entry.State)
            {
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    entry.Entity.IsDeleted = true;
                    entry.State = EntityState.Modified;
                    break;
                case EntityState.Modified:
                    break;
                case EntityState.Added:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
    }

    private void WriteTime()
    {
        foreach (var entry in ChangeTracker.Entries<ITimedEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedOn = DateTime.UtcNow;
                    break;
                case EntityState.Added:
                    entry.Entity.CreatedOn = DateTime.UtcNow.ToUniversalTime();
                    entry.Entity.ModifiedOn = DateTime.UtcNow.ToUniversalTime();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}