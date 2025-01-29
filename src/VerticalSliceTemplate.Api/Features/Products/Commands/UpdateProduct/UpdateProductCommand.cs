using System.Text.Json.Serialization;

namespace VerticalSliceTemplate.Api.Features.Products.Commands.UpdateProduct;

internal sealed class UpdateProductCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
}
