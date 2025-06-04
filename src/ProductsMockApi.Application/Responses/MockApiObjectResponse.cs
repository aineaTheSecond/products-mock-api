using System.Text.Json.Serialization;

namespace ProductsMockApi.Application.Responses;

public class MockApiObjectResponse
{
  public string Id { get; set; }
  public string Name { get; set; }
  [JsonPropertyName("data")]
  public Dictionary<string, object> Data { get; set; }
}