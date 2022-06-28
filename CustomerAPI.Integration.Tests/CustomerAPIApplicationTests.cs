namespace CustomerAPI.Integration.Tests;

using CustomerAPI.EndpointDefinitions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class CustomerAPIApplicationTests : WebApplicationFactory<Models.Customer>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var root = new InMemoryDatabaseRoot();

        builder.ConfigureServices(services =>
        {
            services.AddSingleton(sp =>
            {
                // Replace MySql with the in memory provider for tests
                return new DbContextOptionsBuilder<DBContext>()
                            .UseInMemoryDatabase("Customers", root)
                            .UseApplicationServiceProvider(sp)
                            .Options;
            });

            var endpointDefinitions = new List<IEndpointDefinition>();

            endpointDefinitions.AddRange(
                typeof(IEndpointDefinition).Assembly.ExportedTypes
                    .Where(x => typeof(IEndpointDefinition).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                    .Select(Activator.CreateInstance).Cast<IEndpointDefinition>()
            );


            foreach (var endpointDefinition in endpointDefinitions)
            {
                endpointDefinition.DefineServices(services);
            }

            services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IEndpointDefinition>);
        });

        return base.CreateHost(builder);
    }
}
