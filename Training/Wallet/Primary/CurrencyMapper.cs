using Wallet.Domain;

namespace Wallet.Primary;

public static class CurrencyMapper
{
    public static Currency ToDomain(string currency)
    {
        return currency switch
        {
            "EUR" => Currency.Euro,
            "USD" => Currency.Dollar,
            _ => throw new IllegalArgumentException($"Unknown currency: {currency}")
        };
    }
}