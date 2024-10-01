using Microsoft.Extensions.Logging;
using Moq;
using VerticalSliceTemplate.Api.Domain;
using VerticalSliceTemplate.Api.Exceptions;
using VerticalSliceTemplate.Api.Features.Products.Commands.CreateProduct;
using VerticalSliceTemplate.Api.Infrastructure.Repositories.Interfaces;

namespace VerticalSliceTemplate.UnitTests.Products.Commands;

public class CreateProductTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ILogger<CreateProductHandler>> _loggerMock;
    private readonly CreateProductHandler _handler;

    public CreateProductTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loggerMock = new Mock<ILogger<CreateProductHandler>>();

        _handler = new CreateProductHandler(_repositoryMock.Object, _unitOfWorkMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task ShouldCreateNewProduct_WhenProductDoesNotExist()
    {
        // Arrange
        var command = new CreateProductCommand
        {
            Name = "New Product",
            Price = 150m
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New Product", result.Name);
        Assert.Equal(150m, result.Price);
        _repositoryMock.Verify(x => x.Create(It.IsAny<Product>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
