namespace VerticalSliceTemplate.Api.Features.Products.Commands.DeleteProduct;

internal static class DeleteProductEndpoint
{
    internal static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("products/{id:int}", async (int id, IMediator mediator) =>
        {
            await mediator.Send(new DeleteProductCommand(id));
            return Results.NoContent();
        })
        .WithTags(Tags.Products);
    }
}

