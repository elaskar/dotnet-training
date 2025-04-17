namespace Wallet.Tests;

public class InMemoryWalletRepository : IWalletRepository
{
    private readonly Dictionary<WalletId, MyWallet> _wallets = new();

    public MyWallet? Get(WalletId id)
    {
        return _wallets[id];
    }

    public void Save(MyWallet wallet)
    {
        _wallets.Add(wallet.Id, wallet);
    }
}