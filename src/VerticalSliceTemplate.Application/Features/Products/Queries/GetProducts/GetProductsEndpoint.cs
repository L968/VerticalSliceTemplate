using Microsoft.AspNetCore.Mvc;
using VerticalSliceTemplate.Application.Common;
using VerticalSliceTemplate.Application.Common.Endpoints;
using VerticalSliceTemplate.Application.Common.Results;

namespace VerticalSliceTemplate.Application.Features.Products.Queries.GetProducts;

internal sealed class GetProductsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products", async (
                ISender sender,
                CancellationToken cancellationToken,
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 10) =>
            {
                var query = new GetProductsQuery(page, pageSize);

                Result<PaginatedList<GetProductsResponse>> result = await sender.Send(query, cancellationToken);
                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Products);
    }
}
