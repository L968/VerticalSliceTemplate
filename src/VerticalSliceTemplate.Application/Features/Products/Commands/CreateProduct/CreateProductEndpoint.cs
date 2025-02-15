using VerticalSliceTemplate.Application.Common.Endpoints;

namespace VerticalSliceTemplate.Application.Features.Products.Commands.CreateProduct;

internal sealed class CreateProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("product", async (CreateProductCommand command, ISender sender, CancellationToken cancellationToken) =>
        {
            CreateProductResponse response = await sender.Send(command, cancellationToken);

            return Results.Created($"/products/{response.Id}", response);
        })
        .WithTags(Tags.Products);
    }
}
