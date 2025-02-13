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
    private readonly GetProductByIdHandler _handler;

    public GetProductByIdTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        var loggerMock = new Mock<ILogger<GetProductByIdHandler>>();

        _handler = new GetProductByIdHandler(_repositoryMock.Object, loggerMock.Object);
    }

    [Fact]
    public async Task WhenProductExists_ShouldReturnProduct()
    {
        // Arrange
        var investmentProduct = new Product(
            name: "Test Product",
            price: 100m
        );

        _repositoryMock.Setup(x => x.GetByIdAsync(investmentProduct.Id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(investmentProduct);

        var query = new GetProductByIdQuery(Id: investmentProduct.Id);

        // Act
        GetProductByIdResponse result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(investmentProduct.Id, result.Id);
        Assert.Equal("Test Product", result.Name);
        Assert.Equal(100m, result.Price);
    }

    [Fact]
    public async Task WhenProductDoesNotExist_ShouldThrowAppException()
    {
        // Arrange
        var query = new GetProductByIdQuery(Id: Guid.Empty);

        _repositoryMock.Setup(x => x.GetByIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((Product?)null);

        // Act & Assert
        AppException exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(query, CancellationToken.None));
        Assert.Equal(DomainErrors.ProductErrors.ProductNotFound(query.Id).Message, exception.Message);
    }
}
