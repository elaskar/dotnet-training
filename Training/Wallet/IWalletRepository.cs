namespace Wallet;

public interface IWalletRepository
{
    MyWallet? Get(WalletId id);

    void Save(MyWallet wallet);
}