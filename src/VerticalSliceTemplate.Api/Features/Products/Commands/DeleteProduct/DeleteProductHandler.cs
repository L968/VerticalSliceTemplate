using VerticalSliceTemplate.Api.Domain;
using VerticalSliceTemplate.Api.Infrastructure.Repositories.Interfaces;

namespace VerticalSliceTemplate.Api.Features.Products.Commands.DeleteProduct;

internal sealed class DeleteProductHandler(
    IProductRepository repository,
    IUnitOfWork unitOfWork,
    ILogger<DeleteProductHandler> logger
    ) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        Product? investmentProduct = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (investmentProduct is null)
        {
            throw new AppException(DomainErrors.ProductErrors.ProductNotFound(request.Id));
        }

        repository.Delete(investmentProduct);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Successfully deleted Product with Id {Id}", request.Id);
    }
}
