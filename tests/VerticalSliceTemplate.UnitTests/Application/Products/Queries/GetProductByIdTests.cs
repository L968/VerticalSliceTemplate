using Microsoft.Extensions.Logging;
using Moq;
using VerticalSliceTemplate.Application.Common.Exceptions;
using VerticalSliceTemplate.Application.Domain;
using VerticalSliceTemplate.Application.Domain.Products;
using VerticalSliceTemplate.Application.Features.Products.Queries.GetProductById;
using VerticalSliceTemplate.Application.Infrastructure.Database;
using VerticalSliceTemplate.UnitTests.Application.Fixtures;

namespace VerticalSliceTemplate.UnitTests.Application.Products.Queries;

public class GetProductByIdTests : IClassFixture<AppDbContextFixture>
{
    private readonly AppDbContext _dbContext;
    private readonly GetProductByIdHandler _handler;

    public GetProductByIdTests(AppDbContextFixture fixture)
    {
        _dbContext = fixture.DbContext;
        var loggerMock = new Mock<ILogger<GetProductByIdHandler>>();

        _handler = new GetProductByIdHandler(_dbContext, loggerMock.Object);
    }

    [Fact]
    public async Task WhenProductExists_ShouldReturnProduct()
    {
        // Arrange
        var existingProduct = new Product(
            name: "Test Product",
            price: 100m
        );

        await _dbContext.Products.AddAsync(existingProduct);
        await _dbContext.SaveChangesAsync();

        var query = new GetProductByIdQuery(Id: existingProduct.Id);

        // Act
        GetProductByIdResponse result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingProduct.Id, result.Id);
        Assert.Equal("Test Product", result.Name);
        Assert.Equal(100m, result.Price);
    }

    [Fact]
    public async Task WhenProductDoesNotExist_ShouldThrowAppException()
    {
        // Arrange
        var query = new GetProductByIdQuery(Id: Guid.NewGuid());

        // Act & Assert
        AppException exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(query, CancellationToken.None));
        Assert.Equal(DomainErrors.ProductErrors.ProductNotFound(query.Id).Message, exception.Message);
        Assert.Equal(ErrorType.NotFound, exception.ErrorType);
    }
}
