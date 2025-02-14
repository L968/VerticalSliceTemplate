using VerticalSliceTemplate.Api.Domain.Products;

namespace VerticalSliceTemplate.Api.Infrastructure.Database;

internal class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IUnitOfWork
{
    public virtual DbSet<Product> Products { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>()
            .HavePrecision(65, 2);
    }
}
