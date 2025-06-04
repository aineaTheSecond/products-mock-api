using ProductsMockApi.Application.Requests;
using ProductsMockApi.Application.Responses;

namespace ProductsMockApi.Application.Services.Abstractions;

public interface IMockApiService
{
  Task<List<MockApiObjectResponse>> FetchObjectsAsync();
  Task<MockApiObjectResponse> AddObjectAsync(MockCreateObjectRequest request);
  Task<DeleteProductResponse> DeleteObjectAsync(string id);
}