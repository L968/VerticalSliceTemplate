using VerticalSliceTemplate.Application.Common.Results;
using VerticalSliceTemplate.Application.Domain.Products;
using VerticalSliceTemplate.Application.Features.Products.Queries.GetProductById;

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
        Result<GetProductByIdResponse> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(existingProduct.Id, result.Value.Id);
        Assert.Equal("Test Product", result.Value.Name);
        Assert.Equal(100m, result.Value.Price);
    }

    [Fact]
    public async Task WhenProductDoesNotExist_ShouldReturnFailureResult()
    {
        // Arrange
        var query = new GetProductByIdQuery(Id: Guid.NewGuid());

        // Act
        Result<GetProductByIdResponse> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(ProductErrors.ProductNotFound(query.Id).Description, result.Error.Description);
        Assert.Equal(ProductErrors.ProductNotFound(query.Id).Type, result.Error.Type);
    }
}
