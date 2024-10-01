namespace VerticalSliceTemplate.Api.Features.Products.Queries.GetProducts;

public record GetProductsResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
}
