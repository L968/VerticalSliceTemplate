namespace VerticalSliceTemplate.Api.Features.Products.Commands.CreateProduct;

internal sealed record CreateProductCommand(
    string Name,
    decimal Price
) : IRequest<CreateProductResponse>;
