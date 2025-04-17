namespace Wallet.Domain;

public interface IRateProvider
{
    double Rate(StockType from, Currency to);
}