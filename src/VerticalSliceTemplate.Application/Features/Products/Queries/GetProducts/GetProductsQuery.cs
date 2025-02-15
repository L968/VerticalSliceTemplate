namespace VerticalSliceTemplate.Application.Features.Products.Queries.GetProducts;

internal sealed record GetProductsQuery : IRequest<IEnumerable<GetProductsResponse>>;
