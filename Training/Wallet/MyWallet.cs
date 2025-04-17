﻿namespace Wallet;

public enum Currency
{
    Euro,
    Dollar
}

public record WalletId(string Value);

public class MyWallet(WalletId id, params Stock[] stocks)
{
    private readonly List<Stock> _stocks = stocks.ToList();
    public WalletId Id { get; } = id;

    public double Value(Currency currency, IRateProvider rateProvider)
    {
        // return _stocks.Aggregate(0d, (total, stock) => total + stock.Quantity);

        return _stocks.Select(stock => stock.Quantity * rateProvider.Rate(stock.Type, currency)).Sum();
    }
}