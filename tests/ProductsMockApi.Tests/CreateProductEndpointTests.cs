using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using ProductsMockApi.Application.Requests;

namespace ProductsMockApi.Tests;

public class CreateProductEndpointTests(WebApplicationFactory<Program> factory)
  : IClassFixture<WebApplicationFactory<Program>>
{
  private readonly HttpClient _client = factory.CreateClient();

  [Fact]
  public async Task WhenNameIsEmpty_ThenReturnValidationError()
  {
    // Arrange
    var invalidProduct = new CreateProductRequest()
    {
      Name = ""
    };

    // Act
    var response = await _client.PostAsJsonAsync("/api/v1/products", invalidProduct);
    var body = await response.Content.ReadAsStringAsync();

    // Assert
    response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    body.Should().Contain("Product Name is required");
  }

  [Fact]
  public async Task WhenColorIsLessThanThreeCharacters_ThenReturnValidationError()
  {
    // Arrange
    var invalidProduct = new CreateProductRequest()
    {
      Color = ""
    };

    // Act
    var response = await _client.PostAsJsonAsync("/api/v1/products", invalidProduct);
    var body = await response.Content.ReadAsStringAsync();

    // Assert
    response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    body.Should().Contain("Color is too short");
  }

  [Fact]
  public async Task WhenPriceEqualsZero_ThenReturnValidationError()
  {
    // Arrange
    var invalidProduct = new CreateProductRequest()
    {
      Price = 0
    };

    // Act
    var response = await _client.PostAsJsonAsync("/api/v1/products", invalidProduct);
    var body = await response.Content.ReadAsStringAsync();

    // Assert
    response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    body.Should().Contain("Product price should be greater than 0");
  }

  [Fact]
  public async Task WhenCapacityIsEmpty_ThenReturnValidationError()
  {
    // Arrange
    var invalidProduct = new CreateProductRequest()
    {
      Capacity = ""
    };

    // Act
    var response = await _client.PostAsJsonAsync("/api/v1/products", invalidProduct);
    var body = await response.Content.ReadAsStringAsync();

    // Assert
    response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    body.Should().Contain("Capacity is required");
  }

  [Fact]
  public async Task WhenProductIsValid_ThenCreateProduct()
  {
    // Arrange
    var product = new CreateProductRequest
    {
      Name = "Test Product",
      Color = "White",
      Price = 100,
      Capacity = "128 GB"
    };

    // Act
    var response = await _client.PostAsJsonAsync("/api/v1/products", product);

    // Assert
    response.EnsureSuccessStatusCode();
    response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

    var json = await response.Content.ReadAsStringAsync();
    json.Should().Contain("Test Product");
  }
}