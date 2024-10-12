namespace VerticalSliceTemplate.Api.Features.Products.Queries.GetProductById;

public sealed record GetProductByIdResponse(
    int Id,
    string Name,
    decimal Price
);
