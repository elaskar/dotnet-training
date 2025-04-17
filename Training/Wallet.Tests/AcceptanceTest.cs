namespace Wallet.Tests;

public class AcceptanceTest
{
    [Fact]
    public void ShouldGetTheValueOfAWallet()
    {
        var rateProvider = new FakeRateProvider();
        var wallets = new InMemoryWalletRepository();
        var appService = new ApplicationService(wallets, rateProvider);

        var monWallet = new MyWallet(new WalletId("etienne"), new Stock(1, StockType.Euro),
            new Stock(2, StockType.Dollar));
        wallets.Save(monWallet);

        var walletValue = appService.WalletValue(new WalletId("etienne"), Currency.Euro);

        Assert.Equal(2, walletValue);
    }
}