using VerticalSliceTemplate.Application.Common;

namespace VerticalSliceTemplate.Application.Features.Products.Queries.GetProducts;

internal sealed record GetProductsQuery(int Page, int PageSize) : IRequest<Result<PaginatedList<GetProductsResponse>>>;
