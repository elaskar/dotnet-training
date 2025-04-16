namespace Wallet.Tests;

public class FakeRateProvider : IRateProvider
{
    public double Rate(Currency currency, StockType stockType)
    {
        if (currency == Currency.Dollar && stockType == StockType.Euro) return 2;
        if (currency == Currency.Euro && stockType == StockType.Dollar) return 0.5;
        return 1;
    }
}