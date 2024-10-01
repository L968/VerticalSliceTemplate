using System.Text.Json.Serialization;

namespace VerticalSliceTemplate.Api.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
}
