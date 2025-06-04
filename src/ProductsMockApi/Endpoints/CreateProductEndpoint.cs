using FastEndpoints;
using ProductsMockApi.Application.Requests;
using ProductsMockApi.Application.Responses;
using ProductsMockApi.Application.Services.Abstractions;
using ProductsMockApi.Application.Validators;

namespace ProductsMockApi.Endpoints;

public class CreateProductEndpoint(IMockApiService mockApiService)
  : Endpoint<CreateProductRequest, MockApiObjectResponse>
{
  public override void Configure()
  {
    Post("api/v1/products");
    AllowAnonymous();
    Validator<CreateProductRequestValidator>();
  }

  public override async Task HandleAsync(CreateProductRequest req, CancellationToken ct)
  {
    var mockApiRequest = new MockCreateObjectRequest
    {
      Name = req.Name,
      Data = new MockApiObjectData
      {
        Color = req.Color,
        Price = req.Price.ToString("F2"),
        Capacity = req.Capacity
      }
    };

    var mockResponse = await mockApiService.AddObjectAsync(mockApiRequest);

    await SendCreatedAtAsync(nameof(CreateProductEndpoint), new { id = mockResponse?.Id }, mockResponse,
      cancellation: ct);
  }
}