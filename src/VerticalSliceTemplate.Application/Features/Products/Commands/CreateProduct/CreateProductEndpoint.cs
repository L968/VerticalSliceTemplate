using VerticalSliceTemplate.Application.Common.Endpoints;

namespace VerticalSliceTemplate.Application.Features.Products.Commands.CreateProduct;

internal sealed class CreateProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("product", async (CreateProductCommand command, ISender sender, CancellationToken cancellationToken) =>
        {
            Result<CreateProductResponse> result = await sender.Send(command, cancellationToken);

            return result.Match(
                onSuccess: response => Results.Created($"/products/{response.Id}", response),
                onFailure: ApiResults.Problem
            );
        })
        .WithTags(Tags.Products);
    }
}
