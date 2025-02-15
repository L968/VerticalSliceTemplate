namespace VerticalSliceTemplate.Application.Features.Products.Queries.GetProductById;

internal sealed record GetProductByIdResponse(
    Guid Id,
    string Name,
    decimal Price
);
