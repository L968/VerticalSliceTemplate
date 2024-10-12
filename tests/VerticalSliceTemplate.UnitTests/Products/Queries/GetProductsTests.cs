using Microsoft.Extensions.Logging;
using Moq;
using VerticalSliceTemplate.Api.Domain;
using VerticalSliceTemplate.Api.Features.Products.Queries.GetProducts;
using VerticalSliceTemplate.Api.Infrastructure.Repositories.Interfaces;

namespace VerticalSliceTemplate.UnitTests.Products.Queries;

public class GetProductsTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly Mock<ILogger<GetProductsHandler>> _loggerMock;
    private readonly GetProductsHandler _handler;

    public GetProductsTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _loggerMock = new Mock<ILogger<GetProductsHandler>>();

        _handler = new GetProductsHandler(_repositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task ShouldReturnListOfProducts_WhenProductsExist()
    {
        // Arrange
        var investmentProducts = new List<Product>
        {
            new() { Id = 1, Name = "Product A", Price = 100m },
            new() { Id = 2, Name = "Product B", Price = 200m }
        };

        _repositoryMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(investmentProducts);
        var query = new GetProductsQuery();

        // Act
        IEnumerable<GetProductsResponse> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("Product A", result.ElementAt(0).Name);
        Assert.Equal("Product B", result.ElementAt(1).Name);
    }

    [Fact]
    public async Task ShouldReturnEmptyList_WhenNoProductsExist()
    {
        // Arrange
        _repositoryMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync([]);
        var query = new GetProductsQuery();

        // Act
        IEnumerable<GetProductsResponse> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
