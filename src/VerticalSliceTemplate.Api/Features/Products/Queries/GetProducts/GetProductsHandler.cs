using VerticalSliceTemplate.Api.Infrastructure.Repositories.Interfaces;

namespace VerticalSliceTemplate.Api.Features.Products.Queries.GetProducts;

internal sealed class GetProductsHandler(
    IProductRepository repository,
    ILogger<GetProductsHandler> logger
    ) : IRequestHandler<GetProductsQuery, IEnumerable<GetProductsResponse>>
{
    private readonly IProductRepository _repository = repository;
    private readonly ILogger<GetProductsHandler> _logger = logger;

    public async Task<IEnumerable<GetProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync(cancellationToken);

        var response = products
            .Select(p => new GetProductsResponse
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            })
            .ToList();

        _logger.LogInformation("Successfully retrieved {Count} products", response.Count);

        return response;
    }
}
