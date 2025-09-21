using VerticalSliceTemplate.Application.Common;
using VerticalSliceTemplate.Application.Common.Results;
using VerticalSliceTemplate.Application.Domain.Products;
using VerticalSliceTemplate.Application.Features.Products.Queries.GetProducts;

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
    public async Task WhenProductsExist_ShouldReturnPaginatedListOfProducts()
    {
        // Arrange
        _dbContext.Products.RemoveRange(_dbContext.Products);
        await _dbContext.SaveChangesAsync();

        var products = new List<Product>
        {
            new(name: "Product A", price: 100m),
            new(name: "Product B", price: 200m),
            new(name: "Product C", price: 300m)
        };

        await _dbContext.Products.AddRangeAsync(products);
        await _dbContext.SaveChangesAsync();

        var query = new GetProductsQuery(Page: 1, PageSize: 2);

        // Act
        Result<PaginatedList<GetProductsResponse>> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        PaginatedList<GetProductsResponse> paginatedList = result.Value;
        Assert.NotNull(paginatedList);
        Assert.Equal(2, paginatedList.Items.Count());
        Assert.Equal(3, paginatedList.TotalItems);
        Assert.Equal(2, paginatedList.TotalPages);
        Assert.Contains(paginatedList.Items, p => p.Name == "Product A" && p.Price == 100m);
        Assert.Contains(paginatedList.Items, p => p.Name == "Product B" && p.Price == 200m);
    }

    [Fact]
    public async Task WhenNoProductsExist_ShouldReturnEmptyPaginatedList()
    {
        // Arrange
        _dbContext.Products.RemoveRange(_dbContext.Products);
        await _dbContext.SaveChangesAsync();

        var query = new GetProductsQuery(Page: 1, PageSize: 10);

        // Act
        Result<PaginatedList<GetProductsResponse>> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        PaginatedList<GetProductsResponse> paginatedList = result.Value;
        Assert.NotNull(paginatedList);
        Assert.Empty(paginatedList.Items);
        Assert.Equal(0, paginatedList.TotalItems);
        Assert.Equal(0, paginatedList.TotalPages);
    }
}
