using Microsoft.Extensions.Logging;
using Moq;
using VerticalSliceTemplate.Api.Domain;
using VerticalSliceTemplate.Api.Exceptions;
using VerticalSliceTemplate.Api.Features.Products.Queries.GetProductById;
using VerticalSliceTemplate.Api.Infrastructure.Repositories.Interfaces;

namespace VerticalSliceTemplate.UnitTests.Products.Queries;

public class GetProductByIdTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly Mock<ILogger<GetProductByIdHandler>> _loggerMock;
    private readonly GetProductByIdHandler _handler;

    public GetProductByIdTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _loggerMock = new Mock<ILogger<GetProductByIdHandler>>();

        _handler = new GetProductByIdHandler(_repositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var investmentProduct = new Product
        {
            Id = 1,
            Name = "Test Product",
            Price = 100m
        };

        _repositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(investmentProduct);

        var query = new GetProductByIdQuery { Id = 1 };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Product", result.Name);
        Assert.Equal(100m, result.Price);
    }

    [Fact]
    public async Task ShouldThrowAppException_WhenProductDoesNotExist()
    {
        // Arrange
        var query = new GetProductByIdQuery { Id = 999 };

        _repositoryMock.Setup(x => x.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((Product?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(query, CancellationToken.None));
        Assert.Equal("Product with Id 999 not found", exception.Message);
    }
}
