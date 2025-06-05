using FastEndpoints;
using FastEndpoints.Swagger;
using ProductsMockApi.Extensions;
using ProductsMockApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAll", policy =>
  {
    policy.AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader();
  });
});

builder.Services.AddEndpointsApiExplorer()
  .AddFastEndpoints(o => o.IncludeAbstractValidators = true)
  .ConfigureSwagger()
  .AddOptionsConfiguration(builder.Configuration)
  .ConfigureMockApi(builder.Configuration);

var app = builder.Build();

//app.UseDefaultExceptionHandler();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseFastEndpoints(c => c.Versioning.Prefix = "v")
  .UseSwaggerGen()
  .UseCors("AllowAll");

app.Run();

public partial class Program
{
}