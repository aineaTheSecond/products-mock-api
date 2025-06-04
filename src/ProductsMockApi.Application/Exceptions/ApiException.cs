using System.Net;

namespace ProductsMockApi.Application.Exceptions;

public class ApiException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    : Exception(message)
{
  public HttpStatusCode StatusCode { get; } = statusCode;
}