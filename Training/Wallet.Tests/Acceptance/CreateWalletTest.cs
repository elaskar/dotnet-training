using Wallet.Application;
using Wallet.Domain;
using Wallet.Domain.Exception;
using Wallet.Secondary;
using Wallet.Tests.Secondary;
using static Wallet.Tests.Domain.WalletIdFixture;

namespace Wallet.Tests.Acceptance;

public class CreateWalletTest
{
    private readonly ApplicationService _appService;
    private readonly FakeRateProvider _rateProvider = new();
    private readonly InMemoryWalletRepository _wallets = new();

    public CreateWalletTest()
    {
        _appService = new ApplicationService(_wallets, _rateProvider);
    }

    [Fact]
    public void ShouldCreateNewWallet()
    {
        _appService.CreateWallet(LeaWalletId());

        Assert.Equal(0, _appService.WalletValue(LeaWalletId(), Currency.Euro));
    }

    [Fact]
    public void ShouldNotCreateNewWalletWithEmptyId()
    {
        Assert.Throws<EmptyWalletIdException>(() => _appService.CreateWallet(null));
    }

    [Fact]
    public void ShouldNotCreateNewWalletTwice()
    {
        _appService.CreateWallet(LeaWalletId());
        
        Assert.Throws<WalletAlreadyExistsException>(() => _appService.CreateWallet(LeaWalletId()));
    }
}