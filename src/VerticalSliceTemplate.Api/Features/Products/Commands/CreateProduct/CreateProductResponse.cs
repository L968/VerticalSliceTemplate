namespace VerticalSliceTemplate.Api.Features.Products.Commands.CreateProduct;

public sealed record CreateProductResponse(
    Guid Id,
    string Name,
    decimal Price
);
