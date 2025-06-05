using FastEndpoints;
using ProductsMockApi.Application.Responses;
using ProductsMockApi.Application.Services.Abstractions;

namespace ProductsMockApi.Endpoints;

public class DeleteProductEndpoint(IMockApiService mockApiService) : EndpointWithoutRequest<DeleteProductResponse>
{
  public override void Configure()
  {
    Delete("api/v1/products/{id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    var id = Route<string>("id");
    var deleteCarResponse = await mockApiService.DeleteObjectAsync(id);
    await SendOkAsync(deleteCarResponse, ct);
  }
}