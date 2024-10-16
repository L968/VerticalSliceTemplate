using VerticalSliceTemplate.Api.Endpoints;

namespace VerticalSliceTemplate.Api.Features.Products.Commands.DeleteProduct;

internal sealed class DeleteProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("products/{id:Guid}", async (Guid id, IMediator mediator) =>
        {
            await mediator.Send(new DeleteProductCommand(id));
            return Results.NoContent();
        })
        .WithTags(Tags.Products);
    }
}
