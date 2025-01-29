using VerticalSliceTemplate.Api.Domain;

namespace VerticalSliceTemplate.Api.Infrastructure;

internal class AppDbContext : DbContext
{
    public virtual DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>()
            .HavePrecision(65, 2);
    }
}
