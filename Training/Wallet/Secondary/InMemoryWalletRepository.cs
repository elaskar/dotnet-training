using Wallet.Domain;

namespace Wallet.Secondary;

public class InMemoryWalletRepository : IWalletRepository
{
    private readonly Dictionary<WalletId, MyWallet> _wallets = new();

    public MyWallet? Get(WalletId id)
    {
        return _wallets.GetValueOrDefault(id);
    }

    public void Save(MyWallet wallet)
    {
        _wallets.Remove(wallet.Id);
        _wallets.Add(wallet.Id, wallet);
    }

    public bool Contains(WalletId id)
    {
        return _wallets.ContainsKey(id);
    }
}