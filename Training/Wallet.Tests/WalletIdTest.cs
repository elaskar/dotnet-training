using Wallet.Domain;
using Wallet.Domain.Exception;

namespace Wallet.Tests;

public class WalletIdTest
{
    [Fact]
    public void ShouldNotCreateIdWithEmptyString()
    {
        Assert.Throws<EmptyWalletIdException>(() => new WalletId(""));
    }

    [Fact]
    public void ShouldNotCreateIdWithNull()
    {
        Assert.Throws<EmptyWalletIdException>(() => new WalletId(null));
    }
}