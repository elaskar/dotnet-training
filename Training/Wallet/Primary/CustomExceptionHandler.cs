using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Wallet.Domain.Exception;

namespace Wallet.Primary;

public class CustomExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = exception switch
        {
            WalletDoesNotExistException => new ProblemDetails
            {
                Title = "Wallet not found",
                Status = StatusCodes.Status404NotFound,
                Detail = "Wallet not found for id lea"
            },
            IllegalArgumentException => new ProblemDetails
            {
                Title = "Illegal Argument Exception",
                Status = StatusCodes.Status400BadRequest,
                Detail = exception.Message
            },
            _ => new ProblemDetails
            {
                Title = "Missing currency",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "Missing currency"
            }
        };


        httpContext.Response.StatusCode = problemDetails.Status!.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}