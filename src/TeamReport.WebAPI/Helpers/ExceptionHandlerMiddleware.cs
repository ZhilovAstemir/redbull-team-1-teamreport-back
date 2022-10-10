using System.Net;
using System.Text.Json;
using TeamReport.Domain.Exceptions;

namespace TeamReport.WebAPI.Helpers;

public class ExceptionHandlerMiddleware
{
  
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next) =>
        _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (EntityNotFoundException exception)
        {
            await HandleExceptionAsync(context, HttpStatusCode.NotFound, exception.Message);
        }
        catch (DataException exception)
        {
            await HandleExceptionAsync(context, HttpStatusCode.UnprocessableEntity, exception.Message);
        }

    }

    private Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
    {

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var result = JsonSerializer.Serialize(new { error = message });

        return context.Response.WriteAsync(result);
    }
}
