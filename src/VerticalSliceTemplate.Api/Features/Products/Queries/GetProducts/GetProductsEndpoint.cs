namespace VerticalSliceTemplate.Api.Features.Products.Queries.GetProducts;

internal static class GetProductsEndpoint
{
    internal static void MapEndpoint(IEndpointRouteBuilder app)
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

