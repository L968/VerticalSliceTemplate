using VerticalSliceTemplate.Api.Infrastructure.Repositories.Interfaces;

namespace VerticalSliceTemplate.Api.Features.Products.Queries.GetProductById;

internal sealed class GetProductByIdHandler(
    IProductRepository repository,
    ILogger<GetProductByIdHandler> logger
    ) : IRequestHandler<GetProductByIdQuery, GetProductByIdResponse>
{
    private readonly IProductRepository _repository = repository;
    private readonly ILogger<GetProductByIdHandler> _logger = logger;

    public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            throw new AppException($"Product with Id {request.Id} not found");
        }

        _logger.LogInformation("Successfully retrieved  Product with Id {Id}", request.Id);

        return new GetProductByIdResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }
}
