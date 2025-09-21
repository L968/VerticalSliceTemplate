using VerticalSliceTemplate.Application.Common.Results;
using VerticalSliceTemplate.Application.Domain.Products;
using VerticalSliceTemplate.Application.Features.Products.Commands.DeleteProduct;

namespace VerticalSliceTemplate.UnitTests.Application.Products.Commands;

public class DeleteProductTests : IClassFixture<AppDbContextFixture>
{
    private readonly AppDbContext _dbContext;
    private readonly DeleteProductHandler _handler;

    public DeleteProductTests(AppDbContextFixture fixture)
    {
        _dbContext = fixture.DbContext;
        var loggerMock = new Mock<ILogger<DeleteProductHandler>>();

        _handler = new DeleteProductHandler(_dbContext, loggerMock.Object);
    }

    [Fact]
    public async Task WhenProductExists_ShouldDeleteProduct()
    {
        // Arrange
        var existingProduct = new Product(
            name: "Product to Delete",
            price: 100m
        );

        await _dbContext.Products.AddAsync(existingProduct);
        await _dbContext.SaveChangesAsync();

        var command = new DeleteProductCommand(Id: existingProduct.Id);

        // Act
        Result result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Product? deletedProduct = await _dbContext.Products.FindAsync(existingProduct.Id);
        Assert.Null(deletedProduct);
    }

    [Fact]
    public async Task WhenProductDoesNotExist_ShouldReturnFailureResult()
    {
        // Arrange
        var command = new DeleteProductCommand(Id: Guid.NewGuid());

        // Act
        Result result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(ProductErrors.ProductNotFound(command.Id).Description, result.Error.Description);
        Assert.Equal(ProductErrors.ProductNotFound(command.Id).Type, result.Error.Type);
    }
}
