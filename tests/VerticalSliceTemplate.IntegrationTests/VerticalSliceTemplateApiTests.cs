using System.Net.Http.Json;

namespace VerticalSliceTemplate.IntegrationTests;

public class VerticalSliceTemplateApiTests : IClassFixture<VerticalSliceTemplateApiFixture>
{
    private readonly HttpClient _httpClient;

    public VerticalSliceTemplateApiTests(VerticalSliceTemplateApiFixture fixture)
    {
        _httpClient = fixture.HttpClient!;
    }

    [Fact]
    public async Task GetProducts_WhenCalled_ShouldReturnOk()
    {
        // Act
        HttpResponseMessage response = await _httpClient.GetAsync("/products");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetProductById_WhenProductExists_ShouldReturnOk()
    {
        // Arrange
        var createProductCommand = new
        {
            name = "Test Product",
            price = 19.99
        };

        HttpResponseMessage createResponse = await _httpClient.PostAsJsonAsync("/product", createProductCommand);
        CreatedProductResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<CreatedProductResponse>();

        // Act
        HttpResponseMessage response = await _httpClient.GetAsync($"/product/{createdProduct!.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetProductById_WhenProductDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        string nonExistentProductId = Guid.Empty.ToString();

        // Act
        HttpResponseMessage response = await _httpClient.GetAsync($"/product/{nonExistentProductId}");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateProduct_WhenValid_ShouldReturnOk()
    {
        // Arrange
        var createProductCommand = new
        {
            name = "New Product",
            price = 29.99
        };

        // Act
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/product", createProductCommand);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task UpdateProduct_WhenProductExists_ShouldReturnOk()
    {
        // Arrange
        var createProductCommand = new
        {
            name = "Test Product",
            price = 19.99
        };

        HttpResponseMessage createResponse = await _httpClient.PostAsJsonAsync("/product", createProductCommand);
        CreatedProductResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<CreatedProductResponse>();

        var updateProductCommand = new
        {
            name = "Updated Product",
            price = 29.99
        };

        // Act
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"/product/{createdProduct!.Id}", updateProductCommand);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task UpdateProduct_WhenProductDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        string nonExistentProductId = Guid.Empty.ToString();
        var updateProductCommand = new
        {
            name = "Updated Product",
            price = 29.99
        };

        // Act
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"/product/{nonExistentProductId}", updateProductCommand);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteProduct_WhenProductExists_ShouldReturnOk()
    {
        // Arrange
        var createProductCommand = new
        {
            name = "Test Product",
            price = 19.99
        };

        HttpResponseMessage createResponse = await _httpClient.PostAsJsonAsync("/product", createProductCommand);
        CreatedProductResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<CreatedProductResponse>();

        // Act
        HttpResponseMessage response = await _httpClient.DeleteAsync($"/product/{createdProduct!.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteProduct_WhenProductDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        string nonExistentProductId = Guid.Empty.ToString();

        // Act
        HttpResponseMessage response = await _httpClient.DeleteAsync($"/product/{nonExistentProductId}");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}

internal sealed class CreatedProductResponse
{
    public string Id { get; set; } = default!;
}
