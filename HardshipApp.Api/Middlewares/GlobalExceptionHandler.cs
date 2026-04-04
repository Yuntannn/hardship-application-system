using HardshipApp.Common.Exceptions;
using HardshipApp.Common.Helpers;

namespace HardshipApp.Api.Middlewares;
public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private IHostEnvironment _hostEnvironment;
    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger, IHostEnvironment hostEnvironment)
    {
        _next = next;
        _logger = logger;
        _hostEnvironment = hostEnvironment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception exception)
        {
            _logger.LogError(
                exception,
                "Unhandled exception occured  for {Method} {Path}. TraceId: {TraceId}",
                context.Request.Method,
                context.Request.Path,
                context.TraceIdentifier);

            // Build error response
            var (statusCode, response) = BuildErrorResponse(context, exception);
            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(response);
        }
    }

    private (int statusCode, ApiResponse<Object?> Response) BuildErrorResponse(HttpContext context, Exception exception)
    {
        return exception switch
        {
            ApiException apiException => (
                apiException.StatusCode,
                ApiResponse<object?>.FailureResponse(
                    apiException.Message,
                    apiException.StatusCode,
                    NormalizeErrors(apiException.Errors, apiException.Message),
                    context.TraceIdentifier)),
            
            UnauthorizedAccessException => (
                StatusCodes.Status401Unauthorized,
                ApiResponse<object?>.FailureResponse(
                    "Unauthorized access.",
                    StatusCodes.Status401Unauthorized,
                    CreateSingleError("Error", "Unauthorized  access."),
                    context.TraceIdentifier)),

            ArgumentException argumentException =>(
                StatusCodes.Status400BadRequest,
                ApiResponse<object?>.FailureResponse(
                    argumentException.Message,
                    StatusCodes.Status400BadRequest,
                    CreateSingleError("Error", argumentException.Message),
                    context.TraceIdentifier)),

            KeyNotFoundException keyNotFoundException =>(
                StatusCodes.Status404NotFound,
                ApiResponse<object?>.FailureResponse(
                    keyNotFoundException.Message,
                    StatusCodes.Status404NotFound,
                    CreateSingleError("Error", keyNotFoundException.Message),
                    context.TraceIdentifier)),

            _ =>(
                StatusCodes.Status500InternalServerError,
                ApiResponse<object?>.FailureResponse(
                    _hostEnvironment.IsDevelopment() ? exception.Message: "An unexpected error occured.",
                    StatusCodes.Status500InternalServerError,
                    CreateSingleError("Error", _hostEnvironment.IsDevelopment() ? exception.Message: "An unexpected error occured."),
                    context.TraceIdentifier))
        };
    }

    private static IDictionary<string, string[]> NormalizeErrors(IDictionary<string, string[]>? errors, string errorMessage)
    {
        return errors?.Count > 0 ? errors : CreateSingleError("Error", errorMessage);
    }

    private static IDictionary<string, string[]> CreateSingleError(string key, string message)
    {
        return new Dictionary<string, string[]>
        {
            {key,
                new[]
                {
                    message
                }
            }
        };
    }
}