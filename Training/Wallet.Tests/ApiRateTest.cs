namespace Wallet.Tests;

public class ApiRateTest
{
    // private static readonly HttpClient Client = new();

    [Fact]
    private void ShouldGetEuroToDollarRate2()
    {
        var rateProvider = new RestRateProvider();

        var rate = rateProvider.Rate(StockType.Dollar, Currency.Euro);

        Assert.NotEqual(0, rate);
    }

    // [Fact]
    // private async Task ShouldGetEuroToDollarRate()
    // {
    //     Client.BaseAddress = new Uri("https://api.frankfurter.dev/");
    //     Client.DefaultRequestHeaders.Accept.Clear();
    //     Client.DefaultRequestHeaders.Accept.Add(
    //         new MediaTypeWithQualityHeaderValue("application/json"));
    //
    //     var response = await Client.GetAsync("v1/latest?base=EUR&symbols=USD");
    //
    //     Assert.True(response.IsSuccessStatusCode);
    //
    //     var rateResponse = await response.Content.ReadFromJsonAsync<RateResponse>();
    //     Assert.NotNull(rateResponse);
    //     Assert.IsType<double>(rateResponse.Rates.USD);
    //     Assert.True(rateResponse.Rates.USD > 1);
    // }
    //
    // [Fact]
    // private async Task ShouldGetDollarToEuroRate()
    // {
    //     Client.BaseAddress = new Uri("https://api.frankfurter.dev/");
    //     Client.DefaultRequestHeaders.Accept.Clear();
    //     Client.DefaultRequestHeaders.Accept.Add(
    //         new MediaTypeWithQualityHeaderValue("application/json"));
    //
    //     var response = await Client.GetAsync("v1/latest?base=USD&symbols=EUR");
    //
    //     Assert.True(response.IsSuccessStatusCode);
    //
    //     var rateResponse = await response.Content.ReadFromJsonAsync<RateResponse>();
    //     Assert.NotNull(rateResponse);
    //     Assert.IsType<double>(rateResponse.Rates.EUR);
    //     Assert.True(rateResponse.Rates.EUR > 1);
    // }
}