namespace Wallet;

public interface IWalletRepository
{
    MyWallet? Get(WalletId id);
}