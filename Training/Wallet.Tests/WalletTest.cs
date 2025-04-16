namespace Wallet.Tests;

public class WalletTest
{
    [Fact]
    public void ShouldBeValuatedToZeroWhenEmpty()
    {
        MyWallet wallet = new MyWallet();
        
        Assert.Equal(0, wallet.Value(Currency.Euro));
    }
}