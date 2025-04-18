using Wallet.Application;
using Wallet.Domain;
using Wallet.Domain.Exception;
using Wallet.Secondary;
using Wallet.Tests.Domain;
using Wallet.Tests.Secondary;

namespace Wallet.Tests.Acceptance;

public class AddStockTest
{
    private readonly ApplicationService _appService;
    private readonly FakeRateProvider _rateProvider = new();
    private readonly InMemoryWalletRepository _wallets = new();

    public AddStockTest()
    {
        _appService = new ApplicationService(_wallets, _rateProvider);
    }

    [Fact]
    public void ShouldAddStocksToWallet()
    {
        _appService.CreateWallet(WalletIdFixture.LeaWalletId());
        _appService.AddStock(WalletIdFixture.LeaWalletId(), new Stock(2, StockType.Dollar));

        Assert.Equal(2, _appService.WalletValue(WalletIdFixture.LeaWalletId(), Currency.Dollar));
    }

    [Fact]
    public void ShouldNotAddStocksToInexistantWallet()
    {
        Assert.Throws<WalletDoesNotExistException>(() =>
            _appService.AddStock(WalletIdFixture.LeaWalletId(), new Stock(2, StockType.Dollar)));
    }
}