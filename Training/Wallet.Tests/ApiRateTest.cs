namespace Wallet.Tests;

public class ApiRateTest
{
    [Fact(Skip = "flemme")]
    private void ShouldGetEuroToDollarRate()
    {
        var rateProvider = new RestRateProvider();

        var rate = rateProvider.Rate(StockType.Dollar, Currency.Euro);

        Assert.NotEqual(0, rate);
    }

    [Fact]
    private void ShouldGetEuroToEuroRate()
    {
        var rateProvider = new RestRateProvider();

        var rate = rateProvider.Rate(StockType.Euro, Currency.Euro);

        Assert.Equal(1, rate);
    }
}