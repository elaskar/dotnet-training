namespace Wallet;

public class ApplicationService(IWalletRepository wallets, IRateProvider rateProvider)
{
    public double WalletValue(WalletId id, Currency currency)
    {
        return wallets.Get(id)!.Value(currency, rateProvider);
    }
}