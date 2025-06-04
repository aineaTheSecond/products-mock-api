using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductsMockApi.Application.Models;
using ProductsMockApi.Application.Requests;
using ProductsMockApi.Application.Responses;
using ProductsMockApi.Application.Services.Abstractions;

namespace ProductsMockApi.Endpoints;

public class FetchProductsEndpoint(IMockApiService mockApiService)
    : Endpoint<FetchProductsRequest, PagedResult<ProductResponse>>
{
  public override void Configure()
  {
    Get("api/v1/products");
    AllowAnonymous();
  }

  public override async Task HandleAsync(FetchProductsRequest request,
      CancellationToken cancellationToken)
  {
    var response = await mockApiService.FetchObjectsAsync();

    var filteredResponse = response.Skip(request.PageNumber - 1).Take(request.PageSize).ToList();

    if (!string.IsNullOrWhiteSpace(request.SearchTerm))
    {
      filteredResponse = filteredResponse
          .Where(p => p.Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    var carsResponse = filteredResponse.Select(r => new ProductResponse
    {
      Id = r.Id,
      Name = r.Name,
      Price = r.Data != null &&
                r.Data.TryGetValue("price",
                    out var price)
            ? price?.ToString() ?? string.Empty
            : string.Empty,
      Color = r.Data != null &&
                r.Data.TryGetValue("color",
                    out var color)
            ? color?.ToString() ?? string.Empty
            : string.Empty,
      Capacity = r.Data != null &&
                   r.Data.TryGetValue("capacity",
                       out var capacity)
            ? capacity?.ToString() ?? string.Empty
            : string.Empty
    }).ToList();

    var pagedResult = new PagedResult<ProductResponse>
    {
      Items = carsResponse,
      Page = 1,
      Limit = carsResponse.Count,
      TotalCount = carsResponse.Count
    };

    await SendOkAsync(pagedResult, cancellationToken);
  }
}