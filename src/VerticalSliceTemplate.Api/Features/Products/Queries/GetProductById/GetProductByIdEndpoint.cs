namespace VerticalSliceTemplate.Api.Features.Products.Queries.GetProductById;

internal static class GetProductByIdEndpoint
{
    internal static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{id:int}", async (Guid id, IMediator mediator) =>
        {
            var query = new GetProductByIdQuery(id);
            GetProductByIdResponse? response = await mediator.Send(query);

            return response is not null
                ? Results.Ok(response)
                : Results.NotFound();
        })
        .WithTags(Tags.Products);
    }
}

