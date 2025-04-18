using Wallet.Domain;
using Wallet.Primary;

namespace Wallet.Tests.Primary;

public class CurrencyMapperTest
{
    [Fact]
    public void ShouldConvertToDomain()
    {
        Assert.Equal(Currency.Euro, CurrencyMapper.ToDomain("EUR"));
        Assert.Equal(Currency.Dollar, CurrencyMapper.ToDomain("USD"));
    }

    [Fact]
    public void ShouldNotConvertToDomainWhenValueIsNotKnown()
    {
        Assert.Throws<IllegalArgumentException>(() => CurrencyMapper.ToDomain("UNK"));
    }
}