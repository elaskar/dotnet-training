using Wallet.Domain;
using Wallet.Domain.Exception;

namespace Wallet.Application;

public class ApplicationService(IWalletRepository wallets, IRateProvider rateProvider)
{
    public double WalletValue(WalletId id, Currency currency)
    {
        var wallet = GetWalletOrThrow(id);
        return wallet.Value(currency, rateProvider);
    }

    public void CreateWallet(WalletId id)
    {
        if (id is null)
            throw new EmptyWalletIdException();

        if (wallets.Contains(id)) throw new WalletAlreadyExistsException();

        var wallet = new MyWallet(id);
        wallets.Save(wallet);
    }

    public void AddStock(WalletId id, Stock stock)
    {
        var wallet = GetWalletOrThrow(id);

        wallet.Add(stock);
        wallets.Save(wallet);
    }

    private MyWallet GetWalletOrThrow(WalletId id)
    {
        var wallet = wallets.Get(id);
        if (wallet == null) throw new WalletDoesNotExistException();
        return wallet;
    }
}