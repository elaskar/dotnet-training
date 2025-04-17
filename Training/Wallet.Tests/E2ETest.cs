using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Wallet.Tests;

public class E2ETest
{
    [Fact]
    public async Task GetWalletValue()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.GetAsync("/wallets/etienne?currency=euro");

        var readFromJsonAsync = await response.Content.ReadFromJsonAsync<WalletValueResponse>();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(readFromJsonAsync);
        // Assert.Equal("{'value':3}", await response.Content.ReadAsStringAsync());
    }
    
    
}