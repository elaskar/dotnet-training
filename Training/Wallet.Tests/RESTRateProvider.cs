using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Wallet.Tests;

public class RestRateProvider : IRateProvider
{
    private static readonly Dictionary<StockType, string> StockTypeToApiLabels = new()
    {
        {
            StockType.Euro, "EUR"
        },
        {
            StockType.Dollar, "USD"
        }
    };

    private static readonly Dictionary<Currency, string> CurrencyToApiLabels = new()
    {
        {
            Currency.Euro, "EUR"
        },
        {
            Currency.Dollar, "USD"
        }
    };

    private static readonly HttpClient Client = new();


    public double Rate(StockType from, Currency to)
    {
        if (StockTypeToApiLabels[from] == CurrencyToApiLabels[to]) return 1;
        Client.BaseAddress = new Uri("https://api.frankfurter.dev/");
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var path = $"v1/latest?base={StockTypeToApiLabels[from]}&symbols={CurrencyToApiLabels[to]}";

        var response = Client.GetAsync(path);

        var rateResponse = response.Result.Content.ReadFromJsonAsync<RateResponse>().Result;

        if (rateResponse == null)
            throw new ApplicationException("Unable to get rate");
        return to switch
        {
            Currency.Dollar => rateResponse.Rates.USD,
            Currency.Euro => rateResponse.Rates.EUR,
            _ => throw new ArgumentOutOfRangeException(nameof(to), to, null)
        };
    }
}

public class RateResponse
{
    public RateRates Rates { get; set; }
}

public class RateRates
{
    public double EUR { get; set; }
    public double USD { get; set; }
}