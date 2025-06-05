using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProductsMockApi.Application.Requests;
using ProductsMockApi.Application.Responses;

namespace ProductsMockApi.Tests;

public class DeleteProductEndpointTests(WebApplicationFactory<Program> factory)
  : IClassFixture<WebApplicationFactory<Program>>
{
  private readonly HttpClient _client = factory.CreateClient();

  [Fact]
  public async Task WhenDeleteProductEndpointIsCalled_ThenItShouldReturnSuccessMessage()
  {
    // Arrange: Create a product first
    var newProduct = new CreateProductRequest
    {
      Name = "Temp Product for Delete",
      Color = "Blue",
      Price = 100,
      Capacity = "128 GB",

    };

    var createResponse = await _client.PostAsJsonAsync("/api/v1/products", newProduct);
    createResponse.EnsureSuccessStatusCode();

    var responseString = await createResponse.Content.ReadAsStringAsync();

    var deserializedResponse = JsonConvert.DeserializeObject<MockApiObjectResponse>(responseString);

    // Act: Delete the product
    var deleteResponse = await _client.DeleteAsync($"/api/v1/products/{deserializedResponse.Id}");

    // Assert
    Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
  }
}