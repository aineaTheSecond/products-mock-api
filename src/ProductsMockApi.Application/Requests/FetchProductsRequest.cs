namespace ProductsMockApi.Application.Requests;

public class FetchProductsRequest
{
  public string? SearchTerm { get; set; }
  public int PageSize { get; set; } = 10;
  public int PageNumber { get; set; } = 1;
}