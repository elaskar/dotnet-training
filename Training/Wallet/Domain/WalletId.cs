using Wallet.Domain.Exception;

namespace Wallet.Domain;

public record WalletId
{
    public WalletId(string value)
    {
        if (value is null || value.Length == 0) throw new EmptyWalletIdException();
        Value = value;
    }

    public string Value { get; }
}