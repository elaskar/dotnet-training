using Wallet.Domain;

namespace Wallet.Tests;

public class FakeRateProvider : IRateProvider
{
    public double Rate(StockType from, Currency to)
    {
        if (to == Currency.Dollar && from == StockType.Euro) return 2;
        if (to == Currency.Euro && from == StockType.Dollar) return 0.5;
        return 1;
    }
}