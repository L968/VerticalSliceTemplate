using VerticalSliceTemplate.Api.Domain;
using VerticalSliceTemplate.Api.Infrastructure.Repositories.Interfaces;

namespace VerticalSliceTemplate.Api.Features.Products.Queries.GetProducts;

internal sealed class GetProductsHandler(
    IProductRepository repository,
    ILogger<GetProductsHandler> logger
    ) : IRequestHandler<GetProductsQuery, IEnumerable<GetProductsResponse>>
{
    public async Task<IEnumerable<GetProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Product> products = await repository.GetAsync(cancellationToken);

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
