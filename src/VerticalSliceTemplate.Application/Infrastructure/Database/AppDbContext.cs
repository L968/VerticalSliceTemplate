using VerticalSliceTemplate.Application.Domain.Products;

namespace VerticalSliceTemplate.Application.Infrastructure.Database;

internal class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public virtual DbSet<Product> Products { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>()
            .HavePrecision(65, 2);
    }
}
