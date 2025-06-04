namespace ProductsMockApi.Application.Requests;

public class CreateProductRequest
{
  public string Name { get; set; }
  public string Color { get; set; }
  public decimal Price { get; set; }
  public string Capacity { get; set; }
}