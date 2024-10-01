namespace VerticalSliceTemplate.Api.Features.Products.Queries.GetProductById;

public record GetProductByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
}
