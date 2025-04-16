using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Wallet.Tests;

public class RestRateProvider : IRateProvider
{
    private static readonly HttpClient Client = new();

    public double Rate(StockType from, Currency to)
    {
        Client.BaseAddress = new Uri("https://api.frankfurter.dev/");
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var response = Client.GetAsync("v1/latest?base=EUR&symbols=USD");

        var rateResponse = response.Result.Content.ReadFromJsonAsync<RateResponse>().Result;

        return rateResponse.Rates.USD;
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