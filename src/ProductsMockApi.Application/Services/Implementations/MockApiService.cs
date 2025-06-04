using System.Text.Json;
using Microsoft.Extensions.Options;
using ProductsMockApi.Application.Configurations;
using ProductsMockApi.Application.Exceptions;
using ProductsMockApi.Application.Requests;
using ProductsMockApi.Application.Responses;
using ProductsMockApi.Application.Services.Abstractions;

namespace ProductsMockApi.Application.Services.Implementations;

public class MockApiService(HttpClient httpClient, IOptions<MockApiConfiguration> mockApiConfiguration)
    : IMockApiService
{
  private readonly MockApiConfiguration _mockApiConfiguration = mockApiConfiguration.Value;

  public async Task<List<MockApiObjectResponse>> FetchObjectsAsync()
  {
    try
    {
      var url = $"{_mockApiConfiguration.BaseUrl}";

      var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);

      var response = await httpClient.SendAsync(httpRequestMessage);

      if (!response.IsSuccessStatusCode)
        throw new ApiException("Failed to retrieve objects", response.StatusCode);

      var content = await response.Content.ReadAsStringAsync();

      var mockApiResponse = JsonSerializer.Deserialize<List<MockApiObjectResponse>>(content,
          new JsonSerializerOptions
          {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
          });

      if (mockApiResponse == null)
        throw new ApiException("Failed to deserialize the Mock Api response");

      return mockApiResponse;
    }
    catch (Exception e) when (e is not ApiException)
    {
      throw new ApiException(e.Message);
    }
  }

  public async Task<MockApiObjectResponse> AddObjectAsync(MockCreateObjectRequest request)
  {
    try
    {
      var httpRequestMessage = new HttpRequestMessage()
      {
        RequestUri = new Uri(_mockApiConfiguration.BaseUrl),
        Method = HttpMethod.Post,
        Content = new StringContent(JsonSerializer.Serialize(request, new JsonSerializerOptions
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
          PropertyNameCaseInsensitive = true,
        }),
              System.Text.Encoding.UTF8, "application/json")
      };

      var response = await httpClient.SendAsync(httpRequestMessage);

      if (!response.IsSuccessStatusCode)
        throw new ApiException("Failed to add object", response.StatusCode);

      var responseContent = await response.Content.ReadAsStringAsync();
      if (string.IsNullOrEmpty(responseContent))
        throw new ApiException("No content received from the server.", response.StatusCode);

      var deserializedResponse = JsonSerializer.Deserialize<MockApiObjectResponse>(responseContent,
          new JsonSerializerOptions
          {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
          });

      if (deserializedResponse == null)
        throw new ApiException("Failed to deserialize the created object response.");

      return deserializedResponse;
    }
    catch (Exception e) when (e is not ApiException)
    {
      throw new ApiException(e.Message);
    }
  }


  public async Task<DeleteProductResponse> DeleteObjectAsync(string id)
  {
    try
    {
      var httpRequestMessage = new HttpRequestMessage
      {
        Method = HttpMethod.Delete,
        RequestUri = new Uri($"{_mockApiConfiguration.BaseUrl}/{id}")
      };

      var response = await httpClient.SendAsync(httpRequestMessage);

      if (!response.IsSuccessStatusCode)
        throw new ApiException("Failed to delete object", response.StatusCode);

      var responseContent = await response.Content.ReadAsStringAsync();
      if (string.IsNullOrEmpty(responseContent))
        throw new ApiException("No content received from the server.", response.StatusCode);

      var deleteResponse = JsonSerializer.Deserialize<DeleteProductResponse>(responseContent,
          new JsonSerializerOptions
          {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
          });

      if (deleteResponse == null)
        throw new ApiException("Failed to deserialize the delete response.");

      return deleteResponse;
    }
    catch (Exception e) when (e is not ApiException)
    {
      throw new ApiException(e.Message);
    }
  }
}