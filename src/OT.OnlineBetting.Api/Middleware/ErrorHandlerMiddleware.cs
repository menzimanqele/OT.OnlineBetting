using Microsoft.AspNetCore.Diagnostics;

namespace OT.OnlineBetting.Api.Middleware;

public class ErrorHandlerMiddleware : IExceptionHandler
{
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
    {
        _logger = logger;
    }
    
    public  ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var message = exception.Message;
        
        _logger.LogError("Error message: {message}. Time of event {time}. More details {technicalDetails}", message,
            DateTime.UtcNow, exception?.InnerException);
        
        return ValueTask.FromResult(false);
    }
}