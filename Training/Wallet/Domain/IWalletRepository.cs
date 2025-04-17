namespace Wallet.Domain;

public interface IWalletRepository
{
    MyWallet? Get(WalletId id);

    void Save(MyWallet wallet);
}