using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using FastEndpoints.Swagger;
using ProductsMockApi.Application.Configurations;
using ProductsMockApi.Application.Services.Abstractions;
using ProductsMockApi.Application.Services.Implementations;

namespace ProductsMockApi.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection ConfigureMockApi(this IServiceCollection services, IConfiguration configuration)
  {
    var mockApiConfiguration = new MockApiConfiguration();
    configuration.GetSection(MockApiConfiguration.SectionName).Bind(mockApiConfiguration);

    services.AddHttpClient<IMockApiService, MockApiService>(client =>
        {
          client.BaseAddress = new Uri(mockApiConfiguration.BaseUrl);
          client.DefaultRequestHeaders.Accept.Add(
                  new MediaTypeWithQualityHeaderValue("application/json"));
        })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
          ServerCertificateCustomValidationCallback =
                (_, _, _, _) => true
        });

    return services;
  }

  public static IServiceCollection AddOptionsConfiguration(this IServiceCollection services,
      IConfiguration configuration)
  {
    return services.Configure<MockApiConfiguration>(
        configuration.GetSection(MockApiConfiguration.SectionName));
  }

  public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
  {
    services.SwaggerDocument(o =>
    {
      o.DocumentSettings = s =>
          {
          s.Title = "Products Mock Api";
          s.Version = "v1";
        };
      o.SerializerSettings = s =>
          {
          s.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
          s.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        };
    });

    return services;
  }
}