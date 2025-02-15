using VerticalSliceTemplate.Application.Domain.Products;
using VerticalSliceTemplate.Application.Infrastructure.Database;

namespace VerticalSliceTemplate.Application.Features.Products.Queries.GetProducts;

internal sealed class GetProductsHandler(
    AppDbContext dbContext,
    ILogger<GetProductsHandler> logger
    ) : IRequestHandler<GetProductsQuery, IEnumerable<GetProductsResponse>>
{
    public async Task<IEnumerable<GetProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Product> products = await dbContext.Products.ToListAsync(cancellationToken);

        var response = products
            .Select(p => new GetProductsResponse(
                p.Id,
                p.Name,
                p.Price
            ))
            .ToList();

        logger.LogInformation("Successfully retrieved {Count} products", response.Count);

        return response;
    }
}
