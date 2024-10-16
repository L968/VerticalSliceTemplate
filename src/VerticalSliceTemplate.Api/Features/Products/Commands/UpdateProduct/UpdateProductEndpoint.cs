using VerticalSliceTemplate.Api.Endpoints;

namespace VerticalSliceTemplate.Api.Features.Products.Commands.UpdateProduct;

internal sealed class UpdateProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("products/{id:int}", async (Guid id, [FromBody] UpdateProductCommand command, IMediator mediator) =>
        {
            command.Id = id;
            await mediator.Send(command);

            return Results.NoContent();
        })
        .WithTags(Tags.Products);
    }
}
