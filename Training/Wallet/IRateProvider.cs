namespace Wallet;

public interface IRateProvider
{
    double Rate(Currency currency, StockType stockType);
}