using VerticalSliceTemplate.Api.Domain;

namespace VerticalSliceTemplate.Api.Infrastructure;

public class AppDbContext : DbContext
{
    public virtual DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<decimal>()
            .HavePrecision(65, 2);
    }
}
