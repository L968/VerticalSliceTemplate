using Microsoft.EntityFrameworkCore.ChangeTracking;
using VerticalSliceTemplate.Application.Domain;
using VerticalSliceTemplate.Application.Domain.Products;
using VerticalSliceTemplate.Application.Infrastructure.Products;

namespace VerticalSliceTemplate.Application.Infrastructure.Database;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    internal DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditInfo();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditInfo()
    {
        DateTime utcNow = DateTime.UtcNow;
        IEnumerable<EntityEntry<IAuditableEntity>> entries = ChangeTracker.Entries<IAuditableEntity>();

        foreach (EntityEntry<IAuditableEntity> entry in entries)
        {
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
