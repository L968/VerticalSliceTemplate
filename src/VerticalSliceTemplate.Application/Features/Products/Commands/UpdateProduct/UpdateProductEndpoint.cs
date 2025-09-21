using VerticalSliceTemplate.Application.Common.Endpoints;

namespace VerticalSliceTemplate.Application.Features.Products.Commands.UpdateProduct;

internal sealed class UpdateProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("product/{id:Guid}", async (Guid id, UpdateProductCommand command, ISender sender, CancellationToken cancellationToken) =>
        {
            command.Id = id;

            Result result = await sender.Send(command, cancellationToken);
            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Products);
    }
}
