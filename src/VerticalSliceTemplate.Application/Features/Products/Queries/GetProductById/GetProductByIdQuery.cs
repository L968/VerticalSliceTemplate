namespace VerticalSliceTemplate.Application.Features.Products.Queries.GetProductById;

internal sealed record GetProductByIdQuery(Guid Id) : IRequest<GetProductByIdResponse>;
