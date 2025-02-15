using Microsoft.Extensions.Logging;
using Moq;
using VerticalSliceTemplate.Application.Domain.Products;
using VerticalSliceTemplate.Application.Features.Products.Queries.GetProducts;
using VerticalSliceTemplate.Application.Infrastructure.Database;
using VerticalSliceTemplate.UnitTests.Application.Fixtures;

namespace VerticalSliceTemplate.UnitTests.Application.Products.Queries;

public class GetProductsTests : IClassFixture<AppDbContextFixture>
{
    private readonly AppDbContext _dbContext;
    private readonly GetProductsHandler _handler;

    public GetProductsTests(AppDbContextFixture fixture)
    {
        _dbContext = fixture.DbContext;
        var loggerMock = new Mock<ILogger<GetProductsHandler>>();

        _handler = new GetProductsHandler(_dbContext, loggerMock.Object);
    }

    [Fact]
    public async Task WhenProductsExist_ShouldReturnListOfProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product(name: "Product A", price: 100m),
            new Product(name: "Product B", price: 200m),
        };

        await _dbContext.Products.AddRangeAsync(products);
        await _dbContext.SaveChangesAsync();

        var query = new GetProductsQuery();

        // Act
        IEnumerable<GetProductsResponse> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, p => p.Name == "Product A" && p.Price == 100m);
        Assert.Contains(result, p => p.Name == "Product B" && p.Price == 200m);
    }

    [Fact]
    public async Task WhenNoProductsExist_ShouldReturnEmptyList()
    {
        // Arrange
        _dbContext.Products.RemoveRange(_dbContext.Products);
        await _dbContext.SaveChangesAsync();

        var query = new GetProductsQuery();

        // Act
        IEnumerable<GetProductsResponse> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
