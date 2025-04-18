﻿using System.Net;
using System.Net.Http.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Wallet.Tests;

public class ComponentTest(MyWebApplicationFactory<Program> factory) : IClassFixture<MyWebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient = factory.CreateClient();

    [Fact]
    public async Task GetWalletValueEndpointAnswerOkWithNotNullValue()
    {
        await using var application = new WebApplicationFactory<Program>();


        var response = await _httpClient.GetAsync("/wallets/etienne?currency=EUR");

        var readFromJsonAsync = await response.Content.ReadFromJsonAsync<WalletValueResponse>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(2, readFromJsonAsync!.Value);
        Assert.Equal("""{"value":2}""", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GetWalletValueEndpointAnswerNotFoundWithNullValue()
    {
        await using var application = new WebApplicationFactory<Program>();

        var problemDetails = new ProblemDetails
        {
            Title = "Wallet not found",
            Status = StatusCodes.Status404NotFound,
            Detail = "Wallet not found for id lea"
        };

        var response = await _httpClient.GetAsync("/wallets/lea?currency=EUR");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equivalent(problemDetails, await response.Content.ReadFromJsonAsync<ProblemDetails>());
    }

    [Fact]
    public async Task GetWalletValueWithoutId()
    {
        await using var application = new WebApplicationFactory<Program>();


        var response = await _httpClient.GetAsync("/wallets/");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }


    [Fact]
    public async Task GetWalletValueWithUnknownCurrency()
    {
        await using var application = new WebApplicationFactory<Program>();

        var problemDetails = new ProblemDetails
        {
            Title = "Illegal Argument Exception",
            Status = StatusCodes.Status400BadRequest,
            Detail = "Unknown currency: UNK"
        };

        var response = await _httpClient.GetAsync("/wallets/etienne?currency=UNK");


        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equivalent(problemDetails, await response.Content.ReadFromJsonAsync<ProblemDetails>());
    }


    [Fact]
    public async Task GetWalletValueWithoutCurrency()
    {
        await using var application = new WebApplicationFactory<Program>();
        var problemDetails = new ProblemDetails
        {
            Title = "Illegal Argument Exception",
            Status = StatusCodes.Status400BadRequest,
            Detail = "Missing currency"
        };

        var response = await _httpClient.GetAsync("/wallets/etienne");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equivalent(problemDetails, await response.Content.ReadFromJsonAsync<ProblemDetails>());
    }

    [Fact]
    public async Task GetWalletValueWithoutCurrencyValue()
    {
        await using var application = new WebApplicationFactory<Program>();
        var problemDetails = new ProblemDetails
        {
            Title = "Illegal Argument Exception",
            Status = StatusCodes.Status400BadRequest,
            Detail = "Missing currency"
        };

        var response = await _httpClient.GetAsync("/wallets/etienne?currency=");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equivalent(problemDetails, await response.Content.ReadFromJsonAsync<ProblemDetails>());
    }

    [Fact]
    public async Task ShouldNotCreateAWalletWithoutId()
    {
        await using var application = new WebApplicationFactory<Program>();
        var problemDetails = new ProblemDetails
        {
            Title = "Illegal Argument Exception",
            Status = StatusCodes.Status400BadRequest,
            Detail = "Missing wallet id"
        };

        const string json = "{}";

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/wallets/new", content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equivalent(problemDetails, await response.Content.ReadFromJsonAsync<ProblemDetails>());
    }

    [Fact]
    public async Task ShouldCreateWallet()
    {
        await using var application = new WebApplicationFactory<Program>();

        var problemDetails = new ProblemDetails
        {
            Title = "Wallet already exists",
            Status = StatusCodes.Status409Conflict,
            Detail = "Wallet already exists"
        };

        const string json = """{"id":"lea"}""";


        var content = new StringContent(json, Encoding.UTF8, "application/json");

        await _httpClient.PostAsync("/wallets/new", content);
        var response = await _httpClient.PostAsync("/wallets/new", content);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.Equivalent(problemDetails, await response.Content.ReadFromJsonAsync<ProblemDetails>());
    }

    [Fact]
    public async Task ShouldAddStocksToExistingWallet()
    {
        await using var application = new WebApplicationFactory<Program>();
        
        await CreateLeaWallet();

        const string json = """{"quantity": 5, "type": "EUR"}""";

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/wallets/lea/stocks/add", content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    private async Task CreateLeaWallet()
    {
        const string json = """{"id":"lea"}""";

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        await _httpClient.PostAsync("/wallets/new", content);
    }
}