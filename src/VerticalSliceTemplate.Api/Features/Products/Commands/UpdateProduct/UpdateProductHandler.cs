using VerticalSliceTemplate.Api.Domain;
using VerticalSliceTemplate.Api.Infrastructure.Repositories.Interfaces;

namespace VerticalSliceTemplate.Api.Features.Products.Commands.UpdateProduct;

internal sealed class UpdateProductHandler(
    IProductRepository repository,
    IUnitOfWork unitOfWork,
    ILogger<UpdateProductHandler> logger
    ) : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<UpdateProductHandler> _logger = logger;

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? investmentProduct = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (investmentProduct is null)
        {
            throw new AppException($"No Product found with Id {request.Id}");
        }

        investmentProduct.Name = request.Name;
        investmentProduct.Price = request.Price;

        _repository.Update(investmentProduct);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Successfully updated {@Product}", investmentProduct);
    }
}
