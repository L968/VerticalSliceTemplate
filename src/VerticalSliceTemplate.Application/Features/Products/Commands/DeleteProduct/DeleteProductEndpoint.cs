using VerticalSliceTemplate.Application.Common.Endpoints;

namespace VerticalSliceTemplate.Application.Features.Products.Commands.DeleteProduct;

internal sealed class DeleteProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("product/{id:Guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(new DeleteProductCommand(id), cancellationToken);
            return Results.NoContent();
        })
        .WithTags(Tags.Products);
    }
}
