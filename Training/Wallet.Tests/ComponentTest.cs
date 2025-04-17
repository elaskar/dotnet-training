using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Wallet.Tests;

public class ComponentTest(MyWebApplicationFactory<Program> factory) : IClassFixture<MyWebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient = factory.CreateClient();

    [Fact]
    public async Task GetWalletValueEndpointAnswerOkWithNotNullValue()
    {
        await using var application = new WebApplicationFactory<Program>();

        var response = await _httpClient.GetAsync("/wallets/etienne?currency=euro");

        var readFromJsonAsync = await response.Content.ReadFromJsonAsync<WalletValueResponse>();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(2, readFromJsonAsync!.Value);
        Assert.Equal("""{"value":2}""", await response.Content.ReadAsStringAsync());
    }
}