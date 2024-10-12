namespace VerticalSliceTemplate.Api.Features.Products.Queries.GetProducts;

public sealed record GetProductsResponse(
    int Id,
    string Name,
    decimal Price
);
