using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Wallet.E2E.Tests;

public class E2ETest
{
    [Fact(Skip = "Flemme 2")]
    public async Task GetWalletValueEndpointAnswerOkWithNotNullValue()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.GetAsync("/wallets/etienne?currency=euro");

        var readFromJsonAsync = await response.Content.ReadFromJsonAsync<WalletValueResponse>();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(readFromJsonAsync);
    }
}