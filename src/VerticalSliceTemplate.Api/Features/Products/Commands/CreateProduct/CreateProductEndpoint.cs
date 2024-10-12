namespace VerticalSliceTemplate.Api.Features.Products.Commands.CreateProduct;

internal static class CreateProductEndpoint
{
    internal static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products", async (CreateProductCommand command, ISender sender) =>
        {
            CreateProductResponse response = await sender.Send(command);

            return Results.Created($"/products/{response.Id}", response);
        })
        .WithTags(Tags.Products);
    }
}
