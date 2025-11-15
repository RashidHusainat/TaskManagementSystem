using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagementSystem.API.Exceptions.Handler;

public class CustomExceptionHandler(IAppLogger<CustomExceptionHandler> logger, IHostEnvironment env) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception,
            "Error Message: {exceptionMessage}, Time of occurrence {time}", exception.Message, DateTime.UtcNow);

        var problemDetails = new ProblemDetails()
        {
            Title = exception.GetType().Name,
            Detail = exception.Message,
        };

        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

        if (exception is ValidationException validationException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            problemDetails.Status = httpContext.Response.StatusCode;
            problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
        }
        else
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            problemDetails.Status = httpContext.Response.StatusCode;

            if(env.IsDevelopment())
            problemDetails.Extensions.Add("StackTrace", exception.StackTrace);
        }

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

}

