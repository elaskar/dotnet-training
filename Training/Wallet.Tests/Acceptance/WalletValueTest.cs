using Wallet.Application;
using Wallet.Domain;
using Wallet.Domain.Exception;
using Wallet.Secondary;
using Wallet.Tests.Secondary;
using static Wallet.Tests.Domain.WalletIdFixture;

namespace Wallet.Tests.Acceptance;

public class WalletValueTest
{
    private readonly ApplicationService _appService;
    private readonly FakeRateProvider _rateProvider = new();
    private readonly InMemoryWalletRepository _wallets = new();

    public WalletValueTest()
    {
        _appService = new ApplicationService(_wallets, _rateProvider);
    }

    [Fact]
    public void ShouldGetTheValueOfAWallet()
    {
        var monWallet = new MyWallet(EtienneWalletId(), new Stock(1, StockType.Euro),
            new Stock(2, StockType.Dollar));
        _wallets.Save(monWallet);

        var walletValue = _appService.WalletValue(EtienneWalletId(), Currency.Euro);

        Assert.Equal(2, walletValue);
    }

    [Fact]
    public void ShouldThrowWhenWalletDoesNotExist()
    {
        Assert.Throws<WalletDoesNotExistException>(() => _appService.WalletValue(new WalletId("lea"), Currency.Euro));
    }
}