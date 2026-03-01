
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace SupportHub.Api.Middlewares { 

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        // Hata tipine göre status code ve mesajı belirleyelim
        var statusCode = exception switch
        {
            ValidationException => (int)HttpStatusCode.BadRequest,
            KeyNotFoundException => (int)HttpStatusCode.NotFound,
            InvalidOperationException => (int)HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.InternalServerError
        };

        object response = exception switch
        {
            ValidationException fluentEx => new
            {
                error = "Validation Error",
                details = fluentEx.Errors.Select(x => x.ErrorMessage)
            },

            _ => new { error = exception.Message }
        };

        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsJsonAsync(response);
    }
}

}