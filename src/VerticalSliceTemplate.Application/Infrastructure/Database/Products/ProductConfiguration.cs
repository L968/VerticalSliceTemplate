using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VerticalSliceTemplate.Application.Domain.Products;

namespace VerticalSliceTemplate.Application.Infrastructure.Database.Products;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
