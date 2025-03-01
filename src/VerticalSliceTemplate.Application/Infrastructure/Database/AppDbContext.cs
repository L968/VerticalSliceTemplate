using Microsoft.EntityFrameworkCore.ChangeTracking;
using VerticalSliceTemplate.Application.Domain;
using VerticalSliceTemplate.Application.Domain.Products;
using VerticalSliceTemplate.Application.Infrastructure.Database.Products;

namespace VerticalSliceTemplate.Application.Infrastructure.Database;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    internal DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("products");

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>()
            .HavePrecision(65, 2);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditInfo();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditInfo()
    {
        IEnumerable<EntityEntry<IAuditableEntity>> entries = ChangeTracker.Entries<IAuditableEntity>();

        foreach (EntityEntry<IAuditableEntity> entry in entries)
        {
            DateTime utcNow = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
            {
                entry.Property(e => e.CreatedAtUtc).CurrentValue = utcNow;
                entry.Property(e => e.UpdatedAtUtc).CurrentValue = utcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Property(e => e.UpdatedAtUtc).CurrentValue = utcNow;
            }
        }
    }
}
