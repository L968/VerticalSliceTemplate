namespace VerticalSliceTemplate.Api.Features.Products.Commands.UpdateProduct;

internal static class UpdateProductEndpoint
{
    internal static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("products/{id:int}", async (int id, [FromBody] UpdateProductCommand command, IMediator mediator) =>
        {
            command.Id = id;
            await mediator.Send(command);

            return Results.NoContent();
        })
        .WithTags(Tags.Products);
    }
}

