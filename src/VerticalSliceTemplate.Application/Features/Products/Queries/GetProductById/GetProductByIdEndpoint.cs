using VerticalSliceTemplate.Application.Common.Endpoints;

namespace VerticalSliceTemplate.Application.Features.Products.Queries.GetProductById;

internal sealed class GetProductByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("product/{id:Guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetProductByIdQuery(id);
            GetProductByIdResponse? response = await sender.Send(query, cancellationToken);

            return response is not null
                ? Results.Ok(response)
                : Results.NotFound();
        })
        .WithTags(Tags.Products);
    }
}
