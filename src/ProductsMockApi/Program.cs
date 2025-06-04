using System.Reflection;
using FastEndpoints;
using FastEndpoints.Swagger;
using FluentValidation;
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

app.UseDefaultExceptionHandler();
// app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseFastEndpoints()
    .UseSwaggerGen()
    .UseCors("AllowAll");

app.Run();