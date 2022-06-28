namespace CustomerAPI.Unit.Tests;

using CustomerAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class CustomerAPIApplicationTests : WebApplicationFactory<Customer>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var root = new InMemoryDatabaseRoot();

        builder.ConfigureServices(services =>
        {
            services.AddSingleton(sp =>
            {
                // Replace SQLite with the in memory provider for tests
                return new DbContextOptionsBuilder<DBContext>()
                            .UseInMemoryDatabase("Customers", root)
                            .UseApplicationServiceProvider(sp)
                            .Options;
            });
        });

        return base.CreateHost(builder);
    }
}
