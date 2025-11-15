
using System.Diagnostics;

namespace TaskManagementSystem.API.Middleware;

public class LoggingMiddleware(IAppLogger<LoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var request = context.Request;
        var response = context.Response;

        logger.LogInformation("[START] Handle Request:HTTP {Method} {Path} HOST:{Host}",
            request.Method, request.Path, request.Host);

        var timer = new Stopwatch();
        timer.Start();

        await next(context);

        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
            logger.LogWarning("[PERFORMANCE] The  Request:HTTP {Method} {Path} HOST:{Host} took {TimeTaken} seconds",
                request.Method, request.Path, request.Host, timeTaken);

        logger.LogInformation("[END] Handled Request:HTTP {Method} {Path} HOST:{Host} with HTTP {statusCode}",
             request.Method, request.Path, request.Host, response.StatusCode);
    }
}


