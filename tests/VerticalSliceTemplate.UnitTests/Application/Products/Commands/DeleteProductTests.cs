﻿using Microsoft.Extensions.Logging;
using Moq;
using VerticalSliceTemplate.Application.Common.Exceptions;
using VerticalSliceTemplate.Application.Domain;
using VerticalSliceTemplate.Application.Domain.Products;
using VerticalSliceTemplate.Application.Features.Products.Commands.DeleteProduct;
using VerticalSliceTemplate.Application.Infrastructure.Database;
using VerticalSliceTemplate.UnitTests.Application.Fixtures;

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
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        Product? deletedProduct = await _dbContext.Products.FindAsync(existingProduct.Id);
        Assert.Null(deletedProduct);
    }

    [Fact]
    public async Task WhenProductDoesNotExist_ShouldThrowAppException()
    {
        // Arrange
        var command = new DeleteProductCommand(Id: Guid.NewGuid());

        // Act & Assert
        AppException exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal(DomainErrors.ProductErrors.ProductNotFound(command.Id).Message, exception.Message);
        Assert.Equal(ErrorType.NotFound, exception.ErrorType);
    }
}
