using VerticalSliceTemplate.Application.Domain;
using VerticalSliceTemplate.Application.Domain.Products;
using VerticalSliceTemplate.Application.Infrastructure.Database;

namespace VerticalSliceTemplate.Application.Features.Products.Commands.UpdateProduct;

internal sealed class UpdateProductHandler(
    AppDbContext dbContext,
    ILogger<UpdateProductHandler> logger
    ) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await dbContext.Products.FindAsync([request.Id], cancellationToken);

        if (product is null)
        {
            throw new AppException(DomainErrors.ProductErrors.ProductNotFound(request.Id));
        }

        product.Update(
            request.Name,
            request.Price
        );

        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Successfully updated {@Product}", product);
    }
}
