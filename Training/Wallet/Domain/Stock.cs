namespace Wallet.Domain;

public record Stock(double Quantity, StockType Type);

public enum StockType
{
    Euro,
    Dollar
}