using Wallet.Application;
using Wallet.Domain;
using Wallet.Domain.Exception;
using Wallet.Secondary;

namespace Wallet.Tests;

public class AcceptanceTest
{
    private readonly ApplicationService _appService;
    private readonly FakeRateProvider _rateProvider = new();
    private readonly InMemoryWalletRepository _wallets = new();

    public AcceptanceTest()
    {
        _appService = new ApplicationService(_wallets, _rateProvider);
    }

    [Fact]
    public void ShouldGetTheValueOfAWallet()
    {
        var monWallet = new MyWallet(new WalletId("etienne"), new Stock(1, StockType.Euro),
            new Stock(2, StockType.Dollar));
        _wallets.Save(monWallet);

        var walletValue = _appService.WalletValue(new WalletId("etienne"), Currency.Euro);

        Assert.Equal(2, walletValue);
    }

    [Fact]
    public void ShouldThrowWhenWalletDoesNotExist()
    {
        Assert.Throws<WalletDoesNotExistException>(() => _appService.WalletValue(new WalletId("lea"), Currency.Euro));
    }

    [Fact]
    public void ShouldNotCreateNewWalletWithEmptyId()
    {
        Assert.Throws<EmptyWalletIdException>(() => _appService.CreateWallet(null));
    }

    [Fact]
    public void ShouldCreateNewWallet()
    {
        _appService.CreateWallet(new WalletId("lea"));

        Assert.Equal(0, _appService.WalletValue(new WalletId("lea"), Currency.Euro));
    }
}