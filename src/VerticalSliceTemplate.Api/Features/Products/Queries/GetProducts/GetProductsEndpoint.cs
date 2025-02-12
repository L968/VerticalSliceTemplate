using VerticalSliceTemplate.Api.Endpoints;

namespace VerticalSliceTemplate.Api.Features.Products.Queries.GetProducts;

internal sealed class GetProductsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetProductsQuery();
            IEnumerable<GetProductsResponse> response = await sender.Send(query, cancellationToken);

            return Results.Ok(response);
        })
        .WithTags(Tags.Products);
    }
}
