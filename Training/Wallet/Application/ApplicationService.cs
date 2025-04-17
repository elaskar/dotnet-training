using Wallet.Domain;
using Wallet.Domain.Exception;

namespace Wallet.Application;

public class ApplicationService(IWalletRepository wallets, IRateProvider rateProvider)
{
    public double WalletValue(WalletId id, Currency currency)
    {
        var myWallet = wallets.Get(id);
        if (myWallet == null) throw new WalletDoesNotExistException();
        return myWallet.Value(currency, rateProvider);
    }

    public void CreateWallet(WalletId id)
    {
        if (id is null)
            throw new EmptyWalletIdException();
        var wallet = new MyWallet(id);
        wallets.Save(wallet);
    }
}