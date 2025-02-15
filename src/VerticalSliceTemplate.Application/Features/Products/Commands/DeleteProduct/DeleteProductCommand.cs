namespace VerticalSliceTemplate.Application.Features.Products.Commands.DeleteProduct;

internal sealed record DeleteProductCommand(Guid Id) : IRequest;
