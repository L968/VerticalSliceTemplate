using VerticalSliceTemplate.Application.Domain;
using VerticalSliceTemplate.Application.Domain.Products;
using VerticalSliceTemplate.Application.Infrastructure.Database;

namespace VerticalSliceTemplate.Application.Features.Products.Queries.GetProductById;

internal sealed class GetProductByIdHandler(
    AppDbContext dbContext,
    ILogger<GetProductByIdHandler> logger
    ) : IRequestHandler<GetProductByIdQuery, GetProductByIdResponse>
{
    public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        Product? product = await dbContext.Products
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null)
        {
            throw new AppException(DomainErrors.ProductErrors.ProductNotFound(request.Id));
        }

        logger.LogInformation("Successfully retrieved  Product with Id {Id}", request.Id);

        return new GetProductByIdResponse(
            product.Id,
            product.Name,
            product.Price
        );
    }
}
