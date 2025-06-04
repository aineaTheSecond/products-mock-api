using System.Text.Json.Serialization;

namespace ProductsMockApi.Application.Requests;

public class MockCreateObjectRequest
{
  public string Name { get; set; }
  [JsonPropertyName("data")] public MockApiObjectData Data { get; set; }
}

public class MockApiObjectData
{
  public string Color { get; set; }
  public string Price { get; set; }
  public string Capacity { get; set; }
}