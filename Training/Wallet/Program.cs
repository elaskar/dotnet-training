using Wallet;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRateProvider, RestRateProvider>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.MapGet("/wallets/{id}", (HttpRequest request, IRateProvider rateProvider) =>
{
    var currency = request.Query["currency"];
    var walletId = request.RouteValues["id"];

    var wallet = new MyWallet(new WalletId("etienne"), new Stock(1, StockType.Euro), new Stock(2, StockType.Dollar));

    var walletValue = wallet.Value(Currency.Euro, rateProvider);

    return new WalletValueResponse(walletValue);
});


app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public record WalletValueResponse(double Value)
{
}

public partial class Program
{
}