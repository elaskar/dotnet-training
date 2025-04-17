using Wallet.Domain;

namespace Wallet.Tests.Domain;

public static class WalletIdFixture
{
    public static WalletId EtienneWalletId()
    {
        return new WalletId("etienne");
    }

    public static WalletId LeaWalletId()
    {
        return new WalletId("lea");
    }
}