using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Wallet;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRateProvider, RestRateProvider>();
builder.Services.AddSingleton<ApplicationService, ApplicationService>();
builder.Services.AddSingleton<IWalletRepository, InMemoryWalletRepository>();


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();


var app = builder.Build();
app.UseExceptionHandler();

// Todo : virer le setup
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<IWalletRepository>();
db.Save(new MyWallet(new WalletId("etienne"), new Stock(1, StockType.Euro),
    new Stock(2, StockType.Dollar)));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();


app.MapGet("/wallets/{walletId}", (HttpRequest request, ApplicationService appService, string walletId) =>
{
    var currency = request.Query["currency"].FirstOrDefault();

    if (currency is null || currency.Length == 0)
        throw new IllegalArgumentException("Missing currency");

    var walletValue = appService.WalletValue(new WalletId(walletId),
        CurrencyMapper.ToDomain(currency));
    
    return Results.Ok(new WalletValueResponse(walletValue));
});


app.Run();


public record WalletValueResponse(double Value)
{
}

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

public partial class Program
{
}