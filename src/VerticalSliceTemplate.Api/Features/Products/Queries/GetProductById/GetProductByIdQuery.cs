namespace VerticalSliceTemplate.Api.Features.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<GetProductByIdResponse>;
