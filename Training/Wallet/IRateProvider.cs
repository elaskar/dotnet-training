namespace Wallet;

public interface IRateProvider
{
    double Rate(StockType from, Currency to);
}