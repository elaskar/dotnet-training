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
}