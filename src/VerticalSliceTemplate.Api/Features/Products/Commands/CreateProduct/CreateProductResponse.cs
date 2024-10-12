namespace VerticalSliceTemplate.Api.Features.Products.Commands.CreateProduct;

public sealed record CreateProductResponse(
    int Id,
    string Name,
    decimal Price
);
