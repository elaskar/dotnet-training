﻿namespace Wallet.Tests;

public class WalletTest
{
    private static readonly Stock OneEuro = new(1, StockType.Euro);
    private readonly FakeRateProvider _fakeRateProvider = new();

    // public static IEnumerable<object[]> _cases =
    // [
    //     new object[] { new List<Stock> { OneEuro }, Currency.Euro, 1 }
    // ];

    [Fact]
    public void ShouldBeValuatedToZeroWhenEmpty()
    {
        var wallet = new MyWallet();

        Assert.Equal(0, wallet.Value(Currency.Euro, _fakeRateProvider));
    }

    [Fact]
    public void ShouldBeValuatedToOneEuroWhenTheStockIsOneEuro()
    {
        var stock = new Stock(1, StockType.Euro);
        var wallet = new MyWallet(stock);

        Assert.Equal(1, wallet.Value(Currency.Euro, _fakeRateProvider));
    }

    [Fact]
    public void ShouldBeValuatedToThreeEurosWhenTheStocksAreOneEuroAndTwoEuros()
    {
        var stockOneEuro = new Stock(1, StockType.Euro);
        var stockTwoEuros = new Stock(2, StockType.Euro);

        var wallet = new MyWallet(stockOneEuro, stockTwoEuros);

        Assert.Equal(3, wallet.Value(Currency.Euro, _fakeRateProvider));
    }

    [Fact]
    public void ShouldBeValuatedToTwoDollarsWhenTheStockIsOneEuro()
    {
        var stock = new Stock(1, StockType.Euro);
        var wallet = new MyWallet(stock);

        Assert.Equal(2, wallet.Value(Currency.Dollar, _fakeRateProvider));
    }

    [Fact]
    public void ShouldBeValuatedToFourDollarsWhenTheStockIsTwoEuros()
    {
        var stock = new Stock(2, StockType.Euro);
        var wallet = new MyWallet(stock);

        Assert.Equal(4, wallet.Value(Currency.Dollar, _fakeRateProvider));
    }

    [Fact]
    public void ShouldBeValuatedToFourDollarsWhenTheStockIsFourDollars()
    {
        var stock = new Stock(4, StockType.Dollar);
        var wallet = new MyWallet(stock);

        Assert.Equal(4, wallet.Value(Currency.Dollar, _fakeRateProvider));
    }

    [Fact]
    public void ShouldBeCorrectlyValuatedWithEurosAndDollars()
    {
        var stock1 = new Stock(2, StockType.Dollar);
        var stock2 = new Stock(1, StockType.Euro);

        var wallet = new MyWallet(stock1, stock2);

        Assert.Equal(2, wallet.Value(Currency.Euro, _fakeRateProvider));
    }
}