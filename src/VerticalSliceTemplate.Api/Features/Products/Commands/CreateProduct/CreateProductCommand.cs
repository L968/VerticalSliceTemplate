namespace VerticalSliceTemplate.Api.Features.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<CreateProductResponse>
{
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
}
