using System.Text.Json.Serialization;

namespace VerticalSliceTemplate.Application.Features.Products.Commands.UpdateProduct;

internal sealed record UpdateProductCommand : IRequest<Result>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
}
