using VerticalSliceTemplate.Api.Domain;
using VerticalSliceTemplate.Api.Domain.Products;
using VerticalSliceTemplate.Api.Infrastructure.Database;

namespace VerticalSliceTemplate.Api.Features.Products.Commands.UpdateProduct;

internal sealed class UpdateProductHandler(
    IProductRepository repository,
    IUnitOfWork unitOfWork,
    ILogger<UpdateProductHandler> logger
    ) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            throw new AppException(DomainErrors.ProductErrors.ProductNotFound(request.Id));
        }

        product.Update(
            request.Name,
            request.Price
        );

        repository.Update(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Successfully updated {@Product}", product);
    }
}
