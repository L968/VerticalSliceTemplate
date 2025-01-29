namespace VerticalSliceTemplate.Api.Features.Products.Commands.CreateProduct;

internal sealed record CreateProductResponse(
    Guid Id,
    string Name,
    decimal Price
);
