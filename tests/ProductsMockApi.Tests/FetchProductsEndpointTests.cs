using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ProductsMockApi.Tests;

public class FetchProductsEndpointTests(WebApplicationFactory<Program> factory)
  : IClassFixture<WebApplicationFactory<Program>>
{
  private readonly HttpClient _client = factory.CreateClient();

  [Fact]
  public async Task WhenFetchProductsEndpointIsCalled_ThenItShouldReturnListOfProducts()
  {
    // Arrange
    var url = "/api/v1/products";

    // Act
    var response = await _client.GetAsync(url);

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    var json = await response.Content.ReadAsStringAsync();
    Assert.False(string.IsNullOrWhiteSpace(json));
  }
}