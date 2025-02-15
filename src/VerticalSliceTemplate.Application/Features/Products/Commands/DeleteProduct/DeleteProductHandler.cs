using VerticalSliceTemplate.Application.Domain;
using VerticalSliceTemplate.Application.Domain.Products;
using VerticalSliceTemplate.Application.Infrastructure.Database;

namespace VerticalSliceTemplate.Application.Features.Products.Commands.DeleteProduct;

internal sealed class DeleteProductHandler(
    AppDbContext dbContext,
    ILogger<DeleteProductHandler> logger
    ) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        Product? existingProduct = await dbContext.Products.FindAsync([request.Id], cancellationToken);

        if (existingProduct is null)
        {
            throw new AppException(DomainErrors.ProductErrors.ProductNotFound(request.Id));
        }

        dbContext.Remove(existingProduct);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Successfully deleted Product with Id {Id}", request.Id);
    }
}
