namespace VerticalSliceTemplate.Application.Domain.Products;

internal sealed class Product : IAuditableEntity
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }

    private Product() { }

    public Product(string name, decimal price)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
    }

    public void Update(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}
