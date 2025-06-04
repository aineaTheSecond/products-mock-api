using System.Net;
using System.Text.Json;
using ProductsMockApi.Application.Exceptions;

namespace ProductsMockApi.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await next(context);
    }
    catch (ApiException ex)
    {
      logger.LogError(ex, "API exception occurred.");
      await HandleExceptionAsync(context, ex.StatusCode, ex.Message);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Unhandled exception occurred.");
      await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "An unexpected error occurred.");
    }
  }

  private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
  {
    context.Response.ContentType = "application/problem+json";
    context.Response.StatusCode = (int)statusCode;

    var error = new
    {
      status = statusCode,
      error = message
    };

    await context.Response.WriteAsync(JsonSerializer.Serialize(error));
  }
}