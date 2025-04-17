namespace Wallet;

public class InMemoryWalletRepository : IWalletRepository
{
    private readonly Dictionary<WalletId, MyWallet> _wallets = new();

    public MyWallet? Get(WalletId id)
    {
        return _wallets.GetValueOrDefault(id);
    }

    public void Save(MyWallet wallet)
    {
        _wallets.Add(wallet.Id, wallet);
    }
}