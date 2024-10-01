using VerticalSliceTemplate.Api.Domain;
using VerticalSliceTemplate.Api.Infrastructure.Repositories.Interfaces;

namespace VerticalSliceTemplate.Api.Features.Products.Commands.CreateProduct;

internal sealed class CreateProductHandler(
    IProductRepository repository,
    IUnitOfWork unitOfWork,
    ILogger<CreateProductHandler> logger
    ) : IRequestHandler<CreateProductCommand, CreateProductResponse>
{
    private readonly IProductRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<CreateProductHandler> _logger = logger;

    public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price
        };

        _repository.Create(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Successfully create {@Product}", product);

        return new CreateProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }
}
