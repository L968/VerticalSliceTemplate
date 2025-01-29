namespace VerticalSliceTemplate.Api.Features.Products.Commands.DeleteProduct;

internal sealed record DeleteProductCommand(Guid Id) : IRequest;
