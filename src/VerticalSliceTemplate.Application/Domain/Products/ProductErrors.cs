using VerticalSliceTemplate.Application.Common.Results;

namespace VerticalSliceTemplate.Application.Domain.Products;

internal static class ProductErrors
{
    public static Error ProductAlreadyExists(string productName) =>
        Error.Conflict("Product.ProductAlreadyExists", $"A product with name \"{productName}\" already exists.");

    public static Error ProductNotFound(Guid productId) =>
        Error.NotFound("Product.ProductNotFound", $"The product with identifier \"{productId}\" was not found.");
}
