namespace Wallet;

public enum Currency
{
    Euro,
    Dollar
}

public class MyWallet(params Stock[] stocks)
{
    private readonly List<Stock> _stocks = stocks.ToList();

    public double Value(Currency currency, IRateProvider rateProvider)
    {
        // return _stocks.Aggregate(0d, (total, stock) => total + stock.Quantity);
        return _stocks.Select(stock => stock.Quantity * rateProvider.Rate(currency, stock.Type)).Sum();
    }
}