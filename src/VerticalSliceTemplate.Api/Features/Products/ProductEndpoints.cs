using VerticalSliceTemplate.Api.Features.Products.Commands.CreateProduct;
using VerticalSliceTemplate.Api.Features.Products.Commands.DeleteProduct;
using VerticalSliceTemplate.Api.Features.Products.Commands.UpdateProduct;
using VerticalSliceTemplate.Api.Features.Products.Queries.GetProductById;
using VerticalSliceTemplate.Api.Features.Products.Queries.GetProducts;

namespace VerticalSliceTemplate.Api.Features.Products;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        GetProductsEndpoint.MapEndpoint(app);
        GetProductByIdEndpoint.MapEndpoint(app);
        CreateProductEndpoint.MapEndpoint(app);
        UpdateProductEndpoint.MapEndpoint(app);
        DeleteProductEndpoint.MapEndpoint(app);
    }
}
