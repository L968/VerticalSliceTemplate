namespace VerticalSliceTemplate.Api.Features.Products.Commands.CreateProduct;

public record CreateProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
}
