using VerticalSliceTemplate.Application.Common;
using VerticalSliceTemplate.Application.Infrastructure.Database;

namespace VerticalSliceTemplate.Application.Features.Products.Queries.GetProducts;

internal sealed class GetProductsHandler(
    AppDbContext dbContext,
    ILogger<GetProductsHandler> logger
) : IRequestHandler<GetProductsQuery, Result<PaginatedList<GetProductsResponse>>>
{
    public async Task<Result<PaginatedList<GetProductsResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        int totalItems = await dbContext.Products.CountAsync(cancellationToken);

        List<GetProductsResponse> products = await dbContext.Products
            .AsNoTracking()
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(p => new GetProductsResponse(p.Id, p.Name, p.Price))
            .ToListAsync(cancellationToken);

        logger.LogInformation("Successfully retrieved {Count} products", products.Count);

        return new PaginatedList<GetProductsResponse>(
            request.Page,
            request.PageSize,
            totalItems,
            products
        );
    }
}
