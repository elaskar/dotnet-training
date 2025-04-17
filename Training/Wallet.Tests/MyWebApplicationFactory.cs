using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Wallet.Domain;

namespace Wallet.Tests;

public class MyWebApplicationFactory<T> : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Fake Email Provider
            var rateProvider = services.SingleOrDefault(d => d.ServiceType == typeof(IRateProvider));

            services.Remove(rateProvider!);

            services.AddSingleton<IRateProvider, FakeRateProvider>();

            // Test DB Context
            // var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TodoDbContext>));

            // services.Remove(dbContextDescriptor!);
            //
            // services.AddDbContext<TodoDbContext>(options =>
            // {
            //     var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //     options.UseSqlite($"Data Source={Path.Join(path, "MinimalApiDemoTests.db")}");
            // });
        });

        // builder.UseEnvironment("development");
    }
}