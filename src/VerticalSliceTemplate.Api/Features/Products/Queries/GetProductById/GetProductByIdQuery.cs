namespace VerticalSliceTemplate.Api.Features.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(int Id) : IRequest<GetProductByIdResponse>;
