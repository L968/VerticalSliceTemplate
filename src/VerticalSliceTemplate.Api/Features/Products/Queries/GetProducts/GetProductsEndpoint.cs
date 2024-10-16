using VerticalSliceTemplate.Api.Endpoints;

namespace VerticalSliceTemplate.Api.Features.Products.Queries.GetProducts;

internal sealed class GetProductsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products", async (IMediator mediator) =>
        {
            var query = new GetProductsQuery();
            IEnumerable<GetProductsResponse> response = await mediator.Send(query);

            return Results.Ok(response);
        })
        .WithTags(Tags.Products);
    }
}
