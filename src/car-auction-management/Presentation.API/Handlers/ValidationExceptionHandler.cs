namespace Presentation.API.Handlers;

using System;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class ValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
        {
            return false;
        }

        var problemDetails = new ProblemDetails
        {
            Title = "Something is wrong in the request",
            Status = StatusCodes.Status400BadRequest,
            Extensions = { ["errors"] = validationException.Errors }
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
