using Microsoft.AspNetCore.Diagnostics;

namespace TestProject1.ExceptionHandling;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        // Log the original exception
        _logger.LogError(
            exception,
            "An unhandled exception occurred.");

        // Return a standardized HTTP 500 response
        await Results.Problem(
            statusCode: StatusCodes.Status500InternalServerError,
            title: "An unexpected error occurred.",
            detail: "An internal server error occurred."
        ).ExecuteAsync(httpContext);

        return true;
    }
}