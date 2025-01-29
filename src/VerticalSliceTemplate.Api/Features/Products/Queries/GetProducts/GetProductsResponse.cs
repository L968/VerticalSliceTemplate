namespace VerticalSliceTemplate.Api.Features.Products.Queries.GetProducts;

internal sealed record GetProductsResponse(
    Guid Id,
    string Name,
    decimal Price
);
