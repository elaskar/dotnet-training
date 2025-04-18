using Wallet.Application;
using Wallet.Domain;
using Wallet.Primary;
using Wallet.Secondary;

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

app.MapPost("/wallets/new", async (HttpRequest request, ApplicationService appService) =>
{
    var newWalletRequest = await request.ReadFromJsonAsync<NewWalletRequest>();

    if (newWalletRequest!.Id is null || newWalletRequest.Id.Length == 0)
        throw new IllegalArgumentException("Missing wallet id");

    appService.CreateWallet(new WalletId(newWalletRequest.Id));

    return Results.Created();
});

app.MapPost("/wallets/{walletId}/stocks/add",
    async (HttpRequest request, ApplicationService appService, string walletId) =>
    {
        var addStockRequest = await request.ReadFromJsonAsync<AddStockRequest>();


        appService.AddStock(new WalletId(walletId), new Stock(addStockRequest.Quantity, StockType.Euro));

        return Results.Ok();
    });

app.Run();


public record WalletValueResponse(double Value);

public record NewWalletRequest(string? Id);

public record AddStockRequest(double Quantity, string Type);

public partial class Program;